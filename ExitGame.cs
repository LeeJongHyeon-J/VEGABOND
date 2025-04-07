using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // 게임 종료 함수
    public void QuitGame()
    {
        // 게임 종료 로그 (에디터에서만 표시됨)
        Debug.Log("게임이 종료됩니다.");

        // 게임 종료
        Application.Quit();
    }
}