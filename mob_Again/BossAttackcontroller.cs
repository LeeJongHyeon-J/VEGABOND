using UnityEngine;

public class BossWeaponController : MonoBehaviour
{
    [SerializeField] private Collider2D weaponCollider;
    [SerializeField] private Animator bossAnimator;

    void Start()
    {
        // 시작 시 무기 콜라이더 비활성화
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    // 애니메이션 이벤트에서 호출될 메서드: 무기 공격 시작
    public void EnableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
            Debug.Log("무기 콜라이더 활성화");
        }
    }

    // 애니메이션 이벤트에서 호출될 메서드: 무기 공격 종료
    public void DisableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
            Debug.Log("무기 콜라이더 비활성화");
        }
    }

    // 보스 공격 애니메이션 트리거 메서드
    public void PerformAttack()
    {
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("Attack");
        }
    }
}