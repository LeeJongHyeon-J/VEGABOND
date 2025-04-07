using UnityEngine;

public class portal : MonoBehaviour
{
    [Header("텔레포트 설정")]
    [SerializeField] private Transform destinationPoint; // 이동할 목적지 오브젝트
    [SerializeField] private bool teleportPlayer = true; // 플레이어만 텔레포트할지 여부
    [SerializeField] private string playerTag = "Player"; // 플레이어 태그
    [SerializeField] private bool showGizmos = true; // 씬 뷰에서 목적지 표시 여부
    
    [Header("효과 설정")]
    [SerializeField] private bool useEffect = false; // 효과 사용 여부
    [SerializeField] private ParticleSystem teleportEffect; // 텔레포트 효과
    [SerializeField] private AudioClip teleportSound; // 텔레포트 사운드
    private AudioSource audioSource;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject map2;
    private void Start()
    {
        // destinationPoint가 설정되어 있는지 확인
        if (destinationPoint == null)
        {
            Debug.LogWarning("목적지 포인트가 설정되지 않았습니다!");
        }

        // AudioSource 컴포넌트가 필요한 경우 추가
        if (useEffect && teleportSound != null && !TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 목적지가 설정되지 않은 경우 리턴
        if (destinationPoint == null)
        {
            Debug.LogWarning("목적지 포인트가 설정되지 않았습니다!");
            return;
        }

        // 플레이어 태그 확인이 필요한 경우
        if (teleportPlayer && !other.CompareTag(playerTag))
        {
            return;
        }

        // 텔레포트 실행
        TeleportObject(other.gameObject);
        map.SetActive(true);
        map2.SetActive(false);
    }

    private void TeleportObject(GameObject obj)
    {
        // 텔레포트 효과 재생
        if (useEffect)
        {
            if (teleportEffect != null)
            {
                Instantiate(teleportEffect, obj.transform.position, Quaternion.identity);
            }
            
            if (teleportSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(teleportSound);
            }
        }

        // 오브젝트 위치 이동 (회전값도 동일하게 설정)
        obj.transform.position = destinationPoint.position;
        obj.transform.rotation = destinationPoint.rotation;
    }

    // 씬 뷰에 목적지 위치 표시
    private void OnDrawGizmos()
    {
        if (!showGizmos || destinationPoint == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(destinationPoint.position, 0.5f);
        Gizmos.DrawLine(transform.position, destinationPoint.position);
    }
}