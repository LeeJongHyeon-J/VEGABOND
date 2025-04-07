using UnityEngine;

public class HealthCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어와 충돌 시
        {
            // PlayerHealthUI 스크립트가 부모 또는 현재 게임오브젝트에 있는지 확인
            PlayerHealthUI playerHealth = collision.GetComponentInParent<PlayerHealthUI>();
            
            if (playerHealth != null)
            {
                playerHealth.Heal(1); // 체력 1 회복
                Destroy(gameObject); // 오브젝트 삭제
            }
        }
    }
}