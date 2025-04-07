using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f; // 점프 힘
    [SerializeField] private bool resetVerticalVelocity = true; // 기존 수직 속도를 리셋할지 여부

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            
            if (playerRb != null)
            {
                // 기존 수직 속도를 0으로 만듦 (선택적)
                if (resetVerticalVelocity)
                {
                    Vector2 velocity = playerRb.velocity;
                    velocity.y = 0f;
                    playerRb.velocity = velocity;
                }

                // 위쪽으로 힘을 가함
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    // 디버그용 기즈모
    private void OnDrawGizmos()
    {
        // 점프 방향을 시각적으로 표시
        Gizmos.color = Color.green;
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.up * (jumpForce / 3f);
        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireSphere(end, 0.2f);
    }
}