using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DungeonEndTrigger : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private Transform destinationPoint; // 이동할 목적지 오브젝트

    [Header("페이드 설정")]
    [SerializeField] private float fadeDuration = 0.5f; // 페이드 지속시간
    [SerializeField] private Color fadeColor = Color.black; // 페이드 색상

    private static Image fadeImage; // 페이드에 사용할 UI 이미지
    private static Canvas fadeCanvas; // 페이드 캔버스
    private bool isTransitioning = false; // 전환 중인지 확인하는 플래그

    private void Start()
    {
        // 페이드 UI 이미지가 없다면 생성
        if (fadeImage == null)
        {
            CreateFadeImage();
        }
        // 시작할 때 페이드 이미지를 투명하게 설정
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
    }

    private void CreateFadeImage()
    {
        // 기존 캔버스 확인
        fadeCanvas = FindObjectOfType<Canvas>();
        
        // 캔버스가 없으면 새로 생성
        if (fadeCanvas == null)
        {
            GameObject canvasObj = new GameObject("FadeCanvas");
            fadeCanvas = canvasObj.AddComponent<Canvas>();
            fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        // 페이드 이미지 생성
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(fadeCanvas.transform, false);
        
        fadeImage = imageObj.AddComponent<Image>();
        fadeImage.color = fadeColor;
        
        // UI 입력 방지 설정
        fadeImage.raycastTarget = false;

        // 전체 화면을 덮도록 설정
        RectTransform rect = imageObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;

        // 캔버스 정렬 순서 설정
        fadeCanvas.sortingOrder = 999;

        // 페이드 이미지 초기 설정
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTransitioning)
        {
            if (destinationPoint != null)
            {
                StartCoroutine(TransitionRoutine(collision.transform));
            }
            else
            {
                Debug.LogWarning("목적지 포인트가 설정되지 않았습니다!");
            }
        }
    }

    private IEnumerator TransitionRoutine(Transform player)
    {
        isTransitioning = true;

        // 페이드 아웃 (화면이 어두워짐)
        yield return StartCoroutine(FadeRoutine(0f, 1f));

        // 플레이어 위치 이동
        player.position = destinationPoint.position;

        // 약간의 딜레이 (필요한 경우)
        yield return new WaitForSeconds(0.1f);

        // 페이드 인 (화면이 밝아짐)
        yield return StartCoroutine(FadeRoutine(1f, 0f));

        isTransitioning = false;
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color currentColor = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeDuration;

            // 알파값 변경
            currentColor.a = Mathf.Lerp(startAlpha, endAlpha, normalizedTime);
            fadeImage.color = currentColor;

            yield return null;
        }

        // 정확한 최종값 설정
        currentColor.a = endAlpha;
        fadeImage.color = currentColor;
    }

    // 씬 뷰에서 목적지까지의 경로를 시각적으로 표시
    private void OnDrawGizmos()
    {
        if (destinationPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(destinationPoint.position, 0.5f);
            Gizmos.DrawLine(transform.position, destinationPoint.position);
        }
    }
}