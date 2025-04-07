using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryModeButton : MonoBehaviour
{
    // ��ư ������Ʈ
    private Button storyButton;

    void Start()
    {
        // ��ư ������Ʈ ��������
        storyButton = GetComponent<Button>();

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ����
        storyButton.onClick.AddListener(StartStoryMode);
    }

    // ���丮 ��� ���� �Լ�
    void StartStoryMode()
    {
        // ȿ���� ��� (���û���)
        AudioSource buttonSound = GetComponent<AudioSource>();
        if (buttonSound != null)
        {
            buttonSound.Play();
        }

        // ���丮 ��� ������ ��ȯ
        // "StoryMode"�� ���丮 ��� ���� �̸��Դϴ�. ���� �� �̸����� �������ּ���.
        SceneManager.LoadScene("StoryMode");
    }
}
