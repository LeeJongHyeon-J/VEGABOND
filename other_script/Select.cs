using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject creat;    // 플레이어 닉네임 입력UI
    public Text[] slotText;     // 슬롯버튼 아래에 존재하는 Text들  
    public Text newPlayerName;  // 새로 입력된 플레이어의 닉네임

    bool[] savefile = new bool[3];   // 세이브파일 존재유무 저장

    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단
        for (int i = 0; i < 3; i++) 
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))    // 데이터가 있는 경우
            {
                savefile[i] = true;                               // 해당 슬롯 번호의 bool배열 true로 변환
                DataManager.instance.nowSlot = i;                 // 선택한 슬롯 번호 저장
                DataManager.instance.LoadData();                  // 해당 슬롯 데이터 불러옴
                slotText[i].text = DataManager.instance.nowPlayer.name;    // 버튼에 닉네임 표시
            }
            else    // 데이터가 없는 경우
            {
                slotText[i].text = "비어있음";
            }
        }
        // 불러온 데이터를 초기화시킴.(버튼에 닉네임을 표현하기위함이었기 때문)
        DataManager.instance.DataClear();    
    }

    public void Slot(int number)    // 슬롯의 기능 구현
{
    // 슬롯 번호가 유효한지 확인
    if (number < 0 || number >= savefile.Length)
    {
        Debug.LogError("유효하지 않은 슬롯 번호입니다: " + number);
        return;
    }

    // DataManager 인스턴스가 존재하는지 확인
    if (DataManager.instance != null)
    {
        DataManager.instance.nowSlot = number;    // 슬롯의 번호를 슬롯번호로 입력

        if (savefile[number])    // bool 배열에서 현재 슬롯번호가 true라면 = 데이터 존재한다는 뜻
        {
            DataManager.instance.LoadData();    // 데이터를 로드하고
            GoGame();    // 게임씬으로 이동
        }
        else    // bool 배열에서 현재 슬롯번호가 false라면 데이터가 없다는 뜻
        {
            Creat();    // 플레이어 닉네임 입력 UI 활성화
        }
    }
    else
    {
        Debug.LogError("DataManager 인스턴스가 없습니다!");
    }
}

    public void Creat()    // 플레이어 닉네임 입력 UI를 활성화하는 메소드
    {
        creat.gameObject.SetActive(true);
    }

   public void GoGame()    // 게임씬으로 이동
{
    Debug.Log("현재 선택된 슬롯: " + DataManager.instance.nowSlot); // 디버그용

    if (!savefile[DataManager.instance.nowSlot])    // 현재 슬롯번호의 데이터가 없다면
    {
        if (newPlayerName != null && !string.IsNullOrEmpty(newPlayerName.text))
        {
            // 새로운 플레이어 데이터 생성
            DataManager.instance.nowPlayer.name = newPlayerName.text;
            
            // 현재 슬롯 번호 확인
            Debug.Log("저장 시도 - 슬롯 번호: " + DataManager.instance.nowSlot);
            Debug.Log("저장할 이름: " + newPlayerName.text);
            
            // 데이터 저장
            DataManager.instance.SaveData();
            
            // 저장 성공 여부 확인
            if (File.Exists(DataManager.instance.path + DataManager.instance.nowSlot))
            {
                Debug.Log("저장 성공!");
                savefile[DataManager.instance.nowSlot] = true;
                slotText[DataManager.instance.nowSlot].text = newPlayerName.text;
            }
            else
            {
                Debug.LogError("저장 실패!");
            }
        }
        else
        {
            Debug.LogError("플레이어 이름이 입력되지 않았습니다!");
            return;
        }
    }
    SceneManager.LoadScene(1);
}
}