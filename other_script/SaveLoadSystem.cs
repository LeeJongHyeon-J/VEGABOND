using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSystem : MonoBehaviour
{
    // ���̺� ������ Ŭ���� ����
    [System.Serializable]
    public class SaveData
    {
        public int level;
        public int experience;
        // �ٸ� ������ �����͵�...
    }

    // ���̺� �Լ�
    public void SaveGame(int fileNumber, SaveData data)
    {
        PlayerPrefs.SetInt("File" + fileNumber + "_Level", data.level);
        PlayerPrefs.SetInt("File" + fileNumber + "_Experience", data.experience);
        PlayerPrefs.Save();
        Debug.Log("������ ���� " + fileNumber + "�� ����Ǿ����ϴ�.");
    }

    // �ε� �Լ�
    public SaveData LoadGame(int fileNumber)
    {
        SaveData data = new SaveData();
        data.level = PlayerPrefs.GetInt("File" + fileNumber + "_Level", 1);  // �⺻���� 1�� ����
        data.experience = PlayerPrefs.GetInt("File" + fileNumber + "_Experience", 0);
        Debug.Log("���� " + fileNumber + "���� ������ �ҷ��Խ��ϴ�.");
        return data;
    }
}