using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapSelectionPanel : MonoBehaviour
{
    [System.Serializable]
    public class MapInfo
    {
        public string mapName;
        public Sprite mapThumbnail;
        public string sceneToLoad;
    }

    [Header("Panel Settings")]
    [SerializeField] private GameObject panelRoot; // ��ü �г� ������Ʈ
    [SerializeField] private Transform mapButtonContainer; // �� ��ư���� �� �����̳�
    [SerializeField] private Button closeButton; // �ݱ� ��ư

    [Header("Map Button Prefab")]
    [SerializeField] private GameObject mapButtonPrefab; // �� ��ư ������

    [Header("Maps")]
    [SerializeField] private List<MapInfo> availableMaps = new List<MapInfo>(); // ��� ������ �� ���

    private void Start()
    {
        // ������ �� �г� �����
        panelRoot.SetActive(false);

        // �ݱ� ��ư �̺�Ʈ ����
        closeButton.onClick.AddListener(HidePanel);

        // �� ��ư�� ����
        CreateMapButtons();
    }

    private void CreateMapButtons()
    {
        foreach (MapInfo map in availableMaps)
        {
            GameObject newButton = Instantiate(mapButtonPrefab, mapButtonContainer);

            // ��ư�� �̹��� ����
            Image buttonImage = newButton.transform.Find("Thumbnail")?.GetComponent<Image>();
            if (buttonImage != null && map.mapThumbnail != null)
            {
                buttonImage.sprite = map.mapThumbnail;
            }

            // ��ư�� �ؽ�Ʈ ����
            Text buttonText = newButton.transform.Find("MapName")?.GetComponent<Text>();
            if (buttonText != null)
            {
                buttonText.text = map.mapName;
            }

            // ��ư Ŭ�� �̺�Ʈ ����
            Button button = newButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnMapSelected(map));
            }
        }
    }

    public void ShowPanel()
    {
        panelRoot.SetActive(true);
    }

    public void HidePanel()
    {
        panelRoot.SetActive(false);
    }

    private void OnMapSelected(MapInfo selectedMap)
    {
        Debug.Log($"Selected map: {selectedMap.mapName}");
        // ���⿡ �� �ε� ���� �߰�
        // SceneManager.LoadScene(selectedMap.sceneToLoad);
        HidePanel();
    }
}