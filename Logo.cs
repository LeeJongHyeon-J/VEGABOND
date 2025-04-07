using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LogoSceneController : MonoBehaviour
{
    public Image logoImage;        // �ΰ� �̹��� (UI Image)
    public float fadeDuration = 0.5f; // ���̵� ��/�ƿ� �ð�
    public float displayDuration = 2f; // �ΰ� ���� �ð�

    void Start()
    {
        // �ΰ� ���� ���·� �ʱ�ȭ
        Color color = logoImage.color;
        color.a = 0f; // ���� �� 0���� ���� (����)
        logoImage.color = color;

        // �ִϸ��̼� ����
        StartCoroutine(PlayLogoAnimation());
    }

    IEnumerator PlayLogoAnimation()
    {
        // ���̵� �� (�ΰ� ������ ��Ÿ��)
        yield return StartCoroutine(FadeIn());

        // �ΰ� ���� �ð� ���� ����
        yield return new WaitForSeconds(displayDuration);

        // ���̵� �ƿ� (�ΰ� ������ �����)
        yield return StartCoroutine(FadeOut());

        // ���� ������ ��ȯ
        SceneManager.LoadScene("Anykey"); // ���� �� �̸�
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color color = logoImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration); // ���� �� ����
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
            color.a = Mathf.Clamp01(1f - (elapsed / fadeDuration)); // ���� �� ����
            logoImage.color = color;
            yield return null;
        }
    }
}