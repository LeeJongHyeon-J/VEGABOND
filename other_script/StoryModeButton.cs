using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryModeButton : MonoBehaviour
{
    // 버튼 컴포넌트
    private Button storyButton;

    void Start()
    {
        // 버튼 컴포넌트 가져오기
        storyButton = GetComponent<Button>();

        // 버튼 클릭 이벤트에 함수 연결
        storyButton.onClick.AddListener(StartStoryMode);
    }

    // 스토리 모드 시작 함수
    void StartStoryMode()
    {
        // 효과음 재생 (선택사항)
        AudioSource buttonSound = GetComponent<AudioSource>();
        if (buttonSound != null)
        {
            buttonSound.Play();
        }

        // 스토리 모드 씬으로 전환
        // "StoryMode"는 스토리 모드 씬의 이름입니다. 실제 씬 이름으로 변경해주세요.
        SceneManager.LoadScene("StoryMode");
    }
}
