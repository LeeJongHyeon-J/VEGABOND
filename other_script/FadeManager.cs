using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }
    
    public float fadeDuration = 1.5f;
    private CanvasGroup fadeCanvasGroup;
    private Canvas fadeCanvas;

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFadeScreen();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeFadeScreen()
    {
        // Canvas 생성
        GameObject canvasObj = new GameObject("FadeCanvas");
        fadeCanvas = canvasObj.AddComponent<Canvas>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeCanvas.sortingOrder = 999; // 최상위에 표시
        
        // Canvas Scaler 추가
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        // 검은 화면용 이미지 생성
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvasObj.transform, false);
        Image fadeImage = imageObj.AddComponent<Image>();
        fadeImage.color = Color.black;
        
        // 전체 화면을 덮도록 설정
        RectTransform rect = imageObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        
        // CanvasGroup 추가
        fadeCanvasGroup = imageObj.AddComponent<CanvasGroup>();
        
        // Canvas를 DontDestroyOnLoad로 설정
        DontDestroyOnLoad(canvasObj);
        
        // 씬 변경 이벤트 리스너 추가

        /*SceneManager.sceneLoaded += OnSceneLoaded;*/


    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새로운 씬이 로드될 때마다 페이드 인 실행
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fadeCanvasGroup.alpha = 1f;
        fadeCanvas.enabled = true;

        while (fadeCanvasGroup.alpha > 0f)
        {
            fadeCanvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        fadeCanvas.enabled = false;
    }

    // 필요한 경우 페이드 아웃을 위한 public 메서드
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        fadeCanvas.enabled = true;
        fadeCanvasGroup.alpha = 0f;

        while (fadeCanvasGroup.alpha < 1f)
        {
            fadeCanvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}