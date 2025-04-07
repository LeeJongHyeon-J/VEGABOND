using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LogoSceneController : MonoBehaviour
{
    public Image logoImage;        // 로고 이미지 (UI Image)
    public float fadeDuration = 0.5f; // 페이드 인/아웃 시간
    public float displayDuration = 2f; // 로고 유지 시간

    void Start()
    {
        // 로고를 투명 상태로 초기화
        Color color = logoImage.color;
        color.a = 0f; // 알파 값 0으로 설정 (투명)
        logoImage.color = color;

        // 애니메이션 시작
        StartCoroutine(PlayLogoAnimation());
    }

    IEnumerator PlayLogoAnimation()
    {
        // 페이드 인 (로고가 서서히 나타남)
        yield return StartCoroutine(FadeIn());

        // 로고가 일정 시간 동안 유지
        yield return new WaitForSeconds(displayDuration);

        // 페이드 아웃 (로고가 서서히 사라짐)
        yield return StartCoroutine(FadeOut());

        // 다음 씬으로 전환
        SceneManager.LoadScene("Anykey"); // 다음 씬 이름
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color color = logoImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration); // 알파 값 증가
            logoImage.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color color = logoImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsed / fadeDuration)); // 알파 값 감소
            logoImage.color = color;
            yield return null;
        }
    }
}