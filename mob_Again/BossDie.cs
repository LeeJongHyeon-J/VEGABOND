using UnityEngine;
using System.Collections;

public class BossDie: MonoBehaviour, IDamageable
{
    [Header("체력 설정")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    [Header("피격 효과 설정")]
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private Color hitColor = new Color(1f, 0.3f, 0.3f, 1f);

    [Header("사운드 설정")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] [Range(0f, 1f)] private float soundVolume = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private bool isKnockedBack = false;
    private bool isDead = false;
    public GameObject chest;

    // 애니메이터 파라미터 이름
    private readonly string ANIM_HIT = "Hit";
    private readonly string ANIM_DEATH = "Death";

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = soundVolume;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        animator.SetTrigger(ANIM_HIT);
        PlaySound(hitSound);

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
        
        GetComponent<Collider2D>().enabled = false;
        rb.simulated = false;

        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        float deathAnimationLength = 1f; // 실제 애니메이션 길이로 수정 필요
        yield return new WaitForSeconds(deathAnimationLength);
        
        gameObject.SetActive(false);
        chest.SetActive(true);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public bool IsKnockedBack()
    {
        return isKnockedBack;
    }

    public bool IsDead()
    {
        return isDead;
    }
}