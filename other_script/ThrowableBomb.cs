using UnityEngine;

public class ThrowableBomb : MonoBehaviour
{
    public float throwForce = 400f;
    public float throwAngle = 45f;
    public float explosionRadius = 3f;
    public int explosionDamage = 50;
    public LayerMask damageableLayer;

    private Rigidbody2D rb;
    private bool hasExploded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("[폭탄] Rigidbody2D 컴포넌트가 없습니다!");
            return;
        }

        rb.gravityScale = 1f;
        rb.mass = 1f;
        rb.drag = 0f;
        rb.angularDrag = 0.05f;
        
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player"));
    }

    public void Throw(bool isFacingRight)
    {
        if (rb == null || hasExploded) return;

        float direction = isFacingRight ? 1f : -1f;
        float angleInRadians = throwAngle * Mathf.Deg2Rad;
        
        Vector2 throwForceVector = new Vector2(
            direction * throwForce * Mathf.Cos(angleInRadians),
            throwForce * Mathf.Sin(angleInRadians)
        );
        
        rb.AddForce(throwForceVector, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) return;

        if (!hasExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        hasExploded = true;
        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayer);

        foreach (Collider2D hit in hitColliders)
        {
            // Enemy 레이어인 경우에만 데미지 처리
            if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                float damageFalloff = 1 - (distance / explosionRadius);
                int finalDamage = Mathf.RoundToInt(explosionDamage * damageFalloff);

                //EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
              //  if (enemyHealth != null && finalDamage > 0)
              //  {
              //      enemyHealth.TakeDamage(finalDamage);
              //  }
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}