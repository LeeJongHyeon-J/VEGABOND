using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour  // ShopButton���� UpgradeButton���� Ŭ���� �̸� ����
{
    [SerializeField]
    private Button shopButton;

    [SerializeField]
    private GameObject shopPanel;

    private void Start()
    {
        if (shopButton == null)
        {
            shopButton = GetComponent<Button>();
        }

        if (shopButton != null)
        {
            shopButton.onClick.AddListener(OnShopButtonClick);

            Image buttonImage = shopButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                Color transparent = buttonImage.color;
                transparent.a = 0f;
                buttonImage.color = transparent;
            }
        }

        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }

    private void OnShopButtonClick()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Shop Panel�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }

    private void OnDestroy()
    {
        if (shopButton != null)
        {
            shopButton.onClick.RemoveListener(OnShopButtonClick);
        }
    }
}