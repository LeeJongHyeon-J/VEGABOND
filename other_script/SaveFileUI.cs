
using UnityEngine;
using UnityEngine.UI;

public class SaveFileUI : MonoBehaviour
{
    public SaveLoadSystem saveLoadSystem; // ���̺�/�ε� �ý���
    public GameObject savePanel; // ���� ���� �г�
    public Button[] fileButtons; // ���� ���� ��ư (����1~10�� �ش��ϴ� ��ư �迭)

    // ���̺� �г� Ȱ��ȭ
    public void OpenSavePanel()
    {
        savePanel.SetActive(true);
    }

    // ���̺� ���� ���� �� ȣ��� �Լ�
    public void OnFileSelected(int fileNumber)
    {
        SaveLoadSystem.SaveData data = new SaveLoadSystem.SaveData();
        data.level = 10; // ���÷� ������ ������ ���� (���� 10)
        data.experience = 500; // ����ġ 500
        saveLoadSystem.SaveGame(fileNumber, data); // ������ ���Ͽ� ����
        savePanel.SetActive(false); // �г� �ݱ�
    }

    // �ҷ����� ���� ���� �� ȣ��� �Լ�
    public void OnFileLoadSelected(int fileNumber)
    {
        SaveLoadSystem.SaveData loadedData = saveLoadSystem.LoadGame(fileNumber); // ������ ���Ͽ��� �ҷ�����
        Debug.Log("�ҷ����� �Ϸ� - ����: " + loadedData.level + ", ����ġ: " + loadedData.experience);
        savePanel.SetActive(false); // �г� �ݱ�
    }
}