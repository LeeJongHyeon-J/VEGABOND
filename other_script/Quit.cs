using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{


    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������ ��忡�� �÷��� ��� ����
#else
            Application.Quit(); // ����� ���ӿ����� ���� ����
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
