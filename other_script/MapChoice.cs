using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections;

public class MapChoice : MonoBehaviour
{
    private Image fadeImage;
    private float fadeTime = 1f; // ���̵� �ð� (1��)
    private bool isTransitioning = false;

    void Start()
    {
        // ���̵�� ������ �̹��� ����
        GameObject fadeObj = new GameObject("FadeImage");
        fadeObj.transform.SetParent(transform.root); // Canvas�� �ڽ����� ����

        fadeImage = fadeObj.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0); // ������, ���İ� 0
        fadeImage.raycastTarget = false;

        // ��ü ȭ�� ũ��� ����
        RectTransform rect = fadeImage.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;

        // ��� UI ��ҵ麸�� ���� ���̵��� ����
        fadeImage.transform.SetAsLastSibling();
    }

    // ��ư Ŭ�� �� ȣ���� �Լ�
    public void GoToSceneWithFade(string sceneName)
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeAndLoadScene(sceneName));
        }
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        isTransitioning = true;

        // ���̵� �ƿ� (���� �˰�)
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeTime);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // �� ��ȯ
        SceneManager.LoadScene("MapSelect");
    }
}