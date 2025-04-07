using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionDelay = 2f; // 폭발 지연 시간 (2초)
    public float explosionRadius = 3f; // 폭발 반경
    public int explosionDamage = 50; // 폭발 피해량
    public GameObject explosionEffectPrefab; // 폭발 이펙트 프리팹
    public LayerMask damageableLayer; // 데미지를 받을 수 있는 레이어
    public Animator bombAnimator; // 폭탄 애니메이터

    private void Start()
    {
        // 일정 시간 후 폭발 메서드 호출
        Invoke(nameof(Explode), explosionDelay); // 폭발 호출
    }

    private void Explode()
{
    // 폭발 범위 내의 데미지를 받을 수 있는 오브젝트 검출
    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayer);

    foreach (Collider2D hit in hitColliders)
    {
        // IDamageable 인터페이스를 구현한 컴포넌트 찾기
        IDamageable damageable = hit.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(explosionDamage); // 피해 적용
        }
    }

    // 폭발 이펙트 생성 및 폭탄 삭제
    if (explosionEffectPrefab != null)
    {
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosionEffect, 0.5f);
    }

    Destroy(gameObject); // 폭탄 오브젝트 삭제
}
    private void OnDrawGizmos()
    {
        // 폭발 범위를 시각적으로 표시
        Gizmos.color = Color.red; // 빨간색으로 표시
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // 폭발 범위 그리기
    }
   
    
}