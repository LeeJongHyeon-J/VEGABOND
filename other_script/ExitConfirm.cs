using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitConfirm : MonoBehaviour
{
    public GameObject confirmationPanel;
    public GameObject pausePanel;

    // UI â ����
    public void ShowConfirmation()
    {
        confirmationPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    // UI â �ݱ�
    public void HideConfirmation()
    {
        confirmationPanel.SetActive(false);
    }

    // ���� �޴��� �̵�
    public void ConfirmExit()
    {
        // ���� �޴� ���� �̸��� Ȯ�� �� ����
        SceneManager.LoadScene("MainMenu");
    }
}
