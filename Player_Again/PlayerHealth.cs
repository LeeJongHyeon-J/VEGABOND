using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthUI : MonoBehaviour
{
    public static PlayerHealthUI instance;
    [Header("체력 시스템 설정")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;

    [Header("UI 설정")]
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartsParent;
    [SerializeField] private float heartSpacing = 10f;  // 하트 간의 간격
    private Image[] heartImages;

    [Header("피격 효과 설정")]
    [SerializeField] private float invincibilityDuration = 2f;
    [SerializeField] private float blinkInterval = 0.2f;
    [SerializeField] private Color dimColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    [SerializeField] private GameObject deathUI; // Death UI 패널
    [SerializeField] private Animator animator;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;


    private bool isDead = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitializeHearts();
        currentHealth = maxHealth;
    }

    private void InitializeHearts()
    {
        heartImages = new Image[maxHealth];

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsParent);
            heartImages[i] = newHeart.GetComponent<Image>();
            
            // 하트의 위치를 X축으로 이동
            RectTransform heartRect = newHeart.GetComponent<RectTransform>();
            heartRect.anchoredPosition = new Vector2(i * heartSpacing, 0);
        }

        UpdateHealthUI();
    }

    /// <summary>
    /// 데미지를 받는 함수
    /// </summary>
    /// <param name="damage">받을 데미지 량</param>
    public void TakeDamage(int damage = 1)
    {
        // 무적 상태면 데미지를 받지 않음
        if (isInvincible) return;

        // 체력 감소
        currentHealth = Mathf.Max(0, currentHealth - damage);
        
        // UI 업데이트
        UpdateHealthUI();
        
        // 체력이 0이면 사망
        if (currentHealth <= 0)
        {
            Die();
          
        }
        else
        {
            // 무적 시간 및 깜빡임 효과 시작
            StartCoroutine(InvincibilityCoroutine());
        }
    }


     private IEnumerator ShowDeathUIAfterDelay()
    {
        // 2초 대기
        yield return new WaitForSeconds(2f);
        
        // UI 활성화 및 게임 일시정지
        deathUI.SetActive(true);
        Time.timeScale = 0f;
    }
   

    /// <summary>
    /// 체력 UI를 업데이트하는 함수
    /// </summary>
    private void UpdateHealthUI()
    {
        // 모든 하트 이미지 순회
        for (int i = 0; i < heartImages.Length; i++)
        {
            // 현재 체력보다 작은 인덱스의 하트는 활성화, 나머지는 비활성화
            heartImages[i].enabled = i < currentHealth;
        }
    }

    /// <summary>
    /// 무적 시간 및 깜빡임 효과를 처리하는 코루틴
    /// </summary>
    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        
        // 무적 시간동안 깜빡임 효과 실행
        float endTime = Time.time + invincibilityDuration;
        while (Time.time < endTime)
        {
            // 스프라이트 색상을 어둡게
            spriteRenderer.color = dimColor;
            yield return new WaitForSeconds(blinkInterval / 2);
            
            // 스프라이트 색상을 원래대로
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkInterval / 2);
        }

        // 무적 상태 해제
        isInvincible = false;
    }

    /// <summary>
    /// 체력을 회복하는 함수
    /// </summary>
    /// <param name="amount">회복할 체력량</param>
    public void Heal(int amount = 1)
{
    currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // 체력 회복
    UpdateHealthUI(); // UI 업데이트
}

    /// <summary>
    /// 플레이어 사망 처리 함수
    /// </summary>
    private void Die()
    {
        isDead = true;
         // Death 애니메이션 실행
      //  animator.SetTrigger("Death");
      //  animator.SetBool("IsDead", true); //애니메이션 bool로 적용
        // 코루틴 시작
        StartCoroutine(ShowDeathUIAfterDelay());
    }
       public void restarthp()
    {
       currentHealth = 5; // 체력 회복
       UpdateHealthUI();
    }

    /// <summary>
    /// 현재 무적 상태인지 확인하는 함수
    /// </summary>
    public bool IsInvincible() => isInvincible;
}