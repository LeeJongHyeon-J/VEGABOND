using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // ���� ���� �Լ�
    public void QuitGame()
    {
        // ���� ���� �α� (�����Ϳ����� ǥ�õ�)
        Debug.Log("������ ����˴ϴ�.");

        // ���� ����
        Application.Quit();
    }
}