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
    [SerializeField] private GameObject panelRoot; // 전체 패널 오브젝트
    [SerializeField] private Transform mapButtonContainer; // 맵 버튼들이 들어갈 컨테이너
    [SerializeField] private Button closeButton; // 닫기 버튼

    [Header("Map Button Prefab")]
    [SerializeField] private GameObject mapButtonPrefab; // 맵 버튼 프리팹

    [Header("Maps")]
    [SerializeField] private List<MapInfo> availableMaps = new List<MapInfo>(); // 사용 가능한 맵 목록

    private void Start()
    {
        // 시작할 때 패널 숨기기
        panelRoot.SetActive(false);

        // 닫기 버튼 이벤트 설정
        closeButton.onClick.AddListener(HidePanel);

        // 맵 버튼들 생성
        CreateMapButtons();
    }

    private void CreateMapButtons()
    {
        foreach (MapInfo map in availableMaps)
        {
            GameObject newButton = Instantiate(mapButtonPrefab, mapButtonContainer);

            // 버튼의 이미지 설정
            Image buttonImage = newButton.transform.Find("Thumbnail")?.GetComponent<Image>();
            if (buttonImage != null && map.mapThumbnail != null)
            {
                buttonImage.sprite = map.mapThumbnail;
            }

            // 버튼의 텍스트 설정
            Text buttonText = newButton.transform.Find("MapName")?.GetComponent<Text>();
            if (buttonText != null)
            {
                buttonText.text = map.mapName;
            }

            // 버튼 클릭 이벤트 설정
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
        // 여기에 맵 로드 로직 추가
        // SceneManager.LoadScene(selectedMap.sceneToLoad);
        HidePanel();
    }
}