using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    [Header("몬스터 기본 설정")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    [Header("공격 설정")]
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackCooldown = 2f;
    private float nextAttackTime = 0f;
    [SerializeField] private int attackDamage = 1;

    [Header("피격 효과 설정")]
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private Color hitColor = new Color(1f, 0.3f, 0.3f, 1f);

    [Header("사운드 설정")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] [Range(0f, 1f)] private float soundVolume = 0.5f;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isKnockedBack = false;
    private bool isDead = false;

    private Transform playerTransform;
    private PlayerHealthUI playerHealth;

    // 애니메이터 파라미터 이름
    private readonly string ANIM_ATTACK = "Attack";
    private readonly string ANIM_HIT = "Hit";
    private readonly string ANIM_DEATH = "Death";

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;

        // AudioSource 설정
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = soundVolume;
        }
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform != null)
        {
            playerHealth = playerTransform.GetComponent<PlayerHealthUI>();
        }
    }

    private void Update()
    {
        if (playerTransform == null || isKnockedBack || isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void AttackPlayer()
    {
        if (playerHealth != null)
        {
            animator.SetTrigger(ANIM_ATTACK);
            PlaySound(attackSound);
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        animator.SetTrigger(ANIM_HIT);
        PlaySound(hitSound);

        // 피격 효과
        StartCoroutine(HitEffectCoroutine());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamageWithKnockback(int damage, Vector2 hitPosition)
    {
        if (isDead) return;

        currentHealth -= damage;
        animator.SetTrigger(ANIM_HIT);
        PlaySound(hitSound);

        Vector2 knockbackDirection = ((Vector2)transform.position - hitPosition).normalized;
        StartCoroutine(ApplyKnockback(knockbackDirection));
        StartCoroutine(HitEffectCoroutine());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator ApplyKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        rb.velocity = direction * knockbackForce;
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    private IEnumerator HitEffectCoroutine()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger(ANIM_DEATH);
        PlaySound(deathSound);
        
        // 콜라이더와 리지드바디 비활성화
        GetComponent<Collider2D>().enabled = false;
        rb.simulated = false;

        // 애니메이션 종료 후 오브젝트 제거
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        // 죽음 애니메이션 길이만큼 대기
        float deathAnimationLength = 1f; // 실제 애니메이션 길이로 수정 필요
        yield return new WaitForSeconds(deathAnimationLength);
        
        gameObject.SetActive(false);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}