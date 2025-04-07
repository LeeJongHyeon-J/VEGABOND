using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("이동 설정")]
    public float chaseSpeed = 3f;
    public float detectionRange = 10f;
    public float stopDistance = 2f;

    [Header("공격 설정")]
    public float attackCooldown = 2f;
    public int attackDamage = 1;
    public float attackRange = 2f;

    [Header("애니메이션 설정")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D weaponCollider;

    private Transform player;
    private Rigidbody2D rb;
    private PlayerHealthUI playerHealth;
    private bool isAttacking = false;
    private bool canAttack = true;

    // 애니메이션 해시 값 (성능 최적화)
    private int moveHash = Animator.StringToHash("Move");
    private int attackHash = Animator.StringToHash("Attack");
    private int isMovingHash = Animator.StringToHash("IsMoving");

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        playerHealth = player.GetComponent<PlayerHealthUI>();

        // 무기 콜라이더 초기에 비활성화
        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

    void Update()
    {
        if (player == null) return;

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 감지 범위 내에 있을 때
        if (distanceToPlayer <= detectionRange)
        {
            // 공격 범위 내에 있을 때
            if (distanceToPlayer <= attackRange && canAttack)
            {
                StartAttack();
            }
            // 이동 가능한 상태일 때
            else if (!isAttacking)
            {
                MoveTowardsPlayer(distanceToPlayer);
            }
        }
        else
        {
            // 감지 범위 벗어나면 이동 애니메이션 정지
            StopMoving();
        }
    }

    void MoveTowardsPlayer(float distanceToPlayer)
    {
        // 플레이어 방향으로 이동
        Vector2 direction = (player.position - transform.position).normalized;

        // 멈출 거리보다 멀리 있을 때만 이동
        if (distanceToPlayer > stopDistance)
        {
            rb.MovePosition(rb.position + direction * chaseSpeed * Time.deltaTime);
            
            // 이동 애니메이션 트리거
            animator.SetBool(isMovingHash, true);
            animator.SetTrigger(moveHash);

            // 방향 전환 (스프라이트 방향)
            FlipTowardsPlayer(direction);
        }
        else
        {
            StopMoving();
        }
    }

  void FlipTowardsPlayer(Vector2 direction)
{
    // 현재 스케일 유지하면서 x축 방향만 변경
    Vector3 currentScale = transform.localScale;
    transform.localScale = new Vector3(
        direction.x > 0 ? Mathf.Abs(currentScale.x) : -Mathf.Abs(currentScale.x), 
        currentScale.y, 
        currentScale.z
    );
}

    void StopMoving()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool(isMovingHash, false);
    }

    void StartAttack()
    {
        if (canAttack)
        {
            isAttacking = true;
            canAttack = false;

            // 이동 정지
            StopMoving();

            // 공격 애니메이션 트리거
            animator.SetTrigger(attackHash);

            // 코루틴으로 공격 쿨다운 관리
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    // 애니메이션 이벤트에서 호출될 메서드: 무기 콜라이더 활성화
    public void EnableWeaponCollider()
    {
        if (weaponCollider != null)
            weaponCollider.enabled = true;
    }

    // 애니메이션 이벤트에서 호출될 메서드: 무기 콜라이더 비활성화
    public void DisableWeaponCollider()
    {
        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

    IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        canAttack = true;
    }

    // 기즈모로 감지 범위와 공격 범위 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}