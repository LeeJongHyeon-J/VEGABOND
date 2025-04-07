using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    private Image fadeImage;
    private float fadeTime = 1f; // 페이드 시간 (1초)
    private bool isTransitioning = false;

    void Start()
    {
        // 페이드용 검은색 이미지 생성
        GameObject fadeObj = new GameObject("FadeImage");
        fadeObj.transform.SetParent(transform.root); // Canvas의 자식으로 생성

        fadeImage = fadeObj.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0); // 검은색, 알파값 0
        fadeImage.raycastTarget = false;

        // 전체 화면 크기로 설정
        RectTransform rect = fadeImage.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;

        // 모든 UI 요소들보다 위에 보이도록 설정
        fadeImage.transform.SetAsLastSibling();
    }

    public void GoToScene2()
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        isTransitioning = true;

        // 페이드 아웃 (점점 검게)
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeTime);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 씬 전환
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        // 아무 키나 눌렀을 때 (이미 전환 중이 아니라면)
        if (Input.anyKeyDown && !isTransitioning)
        {
            GoToScene2();
        }
    }
}
