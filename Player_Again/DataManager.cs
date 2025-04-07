using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    // 이름, 레벨, 코인, 착용중인 무기
    public string name;
    public int level = 1;
    public int coin = 100;
    public int item = -1;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // 싱글톤패턴

    public PlayerData nowPlayer = new PlayerData(); // 플레이어 데이터 생성

    public string path; // 경로
    public int nowSlot; // 현재 슬롯번호

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/saveandload";	// 경로 지정
        
        print(path);
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

    public void SaveData()
{
    string data = JsonUtility.ToJson(nowPlayer);
    string path = this.path + nowSlot.ToString();
    File.WriteAllText(path, data);
    Debug.Log($"데이터 저장 완료: 슬롯 {nowSlot}, 경로 {path}");
}

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}

