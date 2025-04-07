using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitConfirm : MonoBehaviour
{
    public GameObject confirmationPanel;
    public GameObject pausePanel;

    // UI 창 열기
    public void ShowConfirmation()
    {
        confirmationPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    // UI 창 닫기
    public void HideConfirmation()
    {
        confirmationPanel.SetActive(false);
    }

    // 메인 메뉴로 이동
    public void ConfirmExit()
    {
        // 메인 메뉴 씬의 이름을 확인 후 적용
        SceneManager.LoadScene("MainMenu");
    }
}
