using UnityEngine;

public class GoldCollector : MonoBehaviour
{
    public int goldAmount = 10; // 기본 획득 골드량
    public AudioClip pickupSound; // 동전 먹는 사운드
    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource를 동적으로 추가하거나 기존 오브젝트에 할당
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // 자동 재생 비활성화
    }

   private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        MoneyManager.instance.AddMoney(goldAmount);
        Debug.Log($"{goldAmount}G 획득! 현재 골드: {MoneyManager.instance.currentMoney}G");

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position); // 위치에서 사운드 재생
        }

        Destroy(gameObject); // 오브젝트 즉시 삭제
    }
}

}
