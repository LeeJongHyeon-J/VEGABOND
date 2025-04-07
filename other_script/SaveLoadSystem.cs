using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSystem : MonoBehaviour
{
    // 세이브 데이터 클래스 정의
    [System.Serializable]
    public class SaveData
    {
        public int level;
        public int experience;
        // 다른 저장할 데이터들...
    }

    // 세이브 함수
    public void SaveGame(int fileNumber, SaveData data)
    {
        PlayerPrefs.SetInt("File" + fileNumber + "_Level", data.level);
        PlayerPrefs.SetInt("File" + fileNumber + "_Experience", data.experience);
        PlayerPrefs.Save();
        Debug.Log("게임이 파일 " + fileNumber + "에 저장되었습니다.");
    }

    // 로드 함수
    public SaveData LoadGame(int fileNumber)
    {
        SaveData data = new SaveData();
        data.level = PlayerPrefs.GetInt("File" + fileNumber + "_Level", 1);  // 기본값을 1로 설정
        data.experience = PlayerPrefs.GetInt("File" + fileNumber + "_Experience", 0);
        Debug.Log("파일 " + fileNumber + "에서 게임을 불러왔습니다.");
        return data;
    }
}