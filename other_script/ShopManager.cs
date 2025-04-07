using UnityEngine;
using UnityEngine.UI;

public class ItemUpgradeManager : MonoBehaviour
{
    // UI 요소들
    public Image iconImage;            // 아이템 아이콘 이미지
    public Text descriptionText;       // 아이템 설명 텍스트
    public Toggle[] upgradeToggles;    // 강화 단계를 표시하는 토글 배열
    public Button upgradeButton;       // 강화 버튼
    public Text priceText;             // 강화 가격 텍스트

    private int currentUpgradeLevel = 0;    // 현재 강화 레벨
    private int basePrice = 100;            // 기본 강화 가격

    void Start()
    {
        UpdateUI();    // 초기 UI 업데이트
        upgradeButton.onClick.AddListener(UpgradeItem);    // 강화 버튼에 클릭 이벤트 리스너 추가
    }

    // UI 업데이트 메서드
    void UpdateUI()
    {
        // 강화 토글 업데이트
        for (int i = 0; i < upgradeToggles.Length; i++)
        {
            upgradeToggles[i].isOn = i < currentUpgradeLevel;
        }

        // 강화 가격 업데이트
        int currentPrice = CalculateUpgradePrice();
        priceText.text = currentPrice.ToString();

        // 설명 텍스트 업데이트 (예시)
        descriptionText.text = $"아이템 레벨: {currentUpgradeLevel}";
    }

    // 강화 가격 계산 메서드
    int CalculateUpgradePrice()
    {
        return basePrice * (currentUpgradeLevel + 1);
    }

    // 아이템 강화 메서드
    void UpgradeItem()
    {
        if (currentUpgradeLevel < upgradeToggles.Length)
        {
            currentUpgradeLevel++;    // 강화 레벨 증가
            UpdateUI();               // UI 업데이트
        }
    }

    // 아이콘 이미지 설정 메서드 (필요시 사용)
    public void SetIcon(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
}