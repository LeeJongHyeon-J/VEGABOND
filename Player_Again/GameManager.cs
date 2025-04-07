using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public GameObject player; // Inspector에서 플레이어 할당

    /*void Start()
    {
        // DataManager에 플레이어 등록
        DataManager.instance.SetPlayer(player);
    }*/
    private void OnApplicationQuit()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.SaveData();
        }
    }
     private static GameManager instance;

    void Awake()
    {
        // 이미 인스턴스가 존재한다면, 중복 방지를 위해 이 객체를 파괴합니다.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스 저장 및 씬 전환 시 삭제되지 않도록 설정
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

  

    public void SaveGame()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.SaveData();
        }
    }

    public void LoadGame()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.LoadData();
        }
    }

    public void ResetGame()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.DataClear();
        }
    }
}