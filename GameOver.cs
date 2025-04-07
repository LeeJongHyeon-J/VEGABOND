using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameObject;
    // 게임 종료
    public void QuitGame()
    {
        Debug.Log("게임을 종료합니다.");
        Application.Quit();
    }

    // 다음 씬으로 이동
    public void NextGame()
    {
        // 시간 스케일을 1로 초기화 (일시정지 등 상태 초기화)
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    // "1map" 씬 재시작
    public void RestartGame()
    {
      
         // 시간 스케일을 1로 초기화 (일시정지 등 상태 초기화)
        Time.timeScale = 1;
        SceneManager.LoadScene("Labo");
    }
}
