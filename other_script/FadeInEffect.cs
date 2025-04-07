using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    public Image fadeImage; // 검은 화면으로 사용할 UI Image
    public float fadeDuration = 2f; // 페이드 인에 걸리는 시간

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        color.a = 1f; // 처음에는 완전히 검은 화면
        fadeImage.color = color;

        // 서서히 투명해지게 만듦
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        // 완전히 투명해지면 이미지 비활성화
        fadeImage.gameObject.SetActive(false);
    }
}