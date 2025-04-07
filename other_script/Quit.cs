using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{


    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 모드에서 플레이 모드 종료
#else
            Application.Quit(); // 빌드된 게임에서는 게임 종료
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
