using UnityEngine;
using System.Collections;

public class PlayerComboSystem : MonoBehaviour 
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private LayerMask enemyLayer;

    // 애니메이터 컴포넌트 참조
    private Animator animator;

    private void Start()
    {
        // 애니메이터 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 왼쪽 컨트롤 키를 누르면 공격
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PerformAttack();
            Debug.Log("공격");
        }
    }

    private void PerformAttack()
    {
        // 공격 애니메이션 트리거
        animator.SetTrigger("Attack");

        // 실제 공격 로직
        Attack();
    }

    private void Attack()
    {
        // 원형의 범위 내에 있는 모든 적을 탐지
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();

            if (damageable != null)
            {
                // 적의 위치와 플레이어의 위치를 이용해 넉백 방향 계산
                Vector2 hitDirection = (enemy.transform.position - transform.position).normalized;
                
                // 데미지와 넉백 구현
                damageable.TakeDamage(attackDamage);
            }
        }
    }

    // 공격 범위를 시각적으로 확인하기 위한 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}