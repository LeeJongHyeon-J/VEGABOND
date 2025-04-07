
using UnityEngine;
using UnityEngine.UI;

public class SaveFileUI : MonoBehaviour
{
    public SaveLoadSystem saveLoadSystem; // 세이브/로드 시스템
    public GameObject savePanel; // 파일 선택 패널
    public Button[] fileButtons; // 파일 선택 버튼 (파일1~10에 해당하는 버튼 배열)

    // 세이브 패널 활성화
    public void OpenSavePanel()
    {
        savePanel.SetActive(true);
    }

    // 세이브 파일 선택 시 호출될 함수
    public void OnFileSelected(int fileNumber)
    {
        SaveLoadSystem.SaveData data = new SaveLoadSystem.SaveData();
        data.level = 10; // 예시로 저장할 데이터 설정 (레벨 10)
        data.experience = 500; // 경험치 500
        saveLoadSystem.SaveGame(fileNumber, data); // 선택한 파일에 저장
        savePanel.SetActive(false); // 패널 닫기
    }

    // 불러오기 파일 선택 시 호출될 함수
    public void OnFileLoadSelected(int fileNumber)
    {
        SaveLoadSystem.SaveData loadedData = saveLoadSystem.LoadGame(fileNumber); // 선택한 파일에서 불러오기
        Debug.Log("불러오기 완료 - 레벨: " + loadedData.level + ", 경험치: " + loadedData.experience);
        savePanel.SetActive(false); // 패널 닫기
    }
}