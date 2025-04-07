using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public Text moneyText; // UI에 표시되는 돈 텍스트
     public Text moneyText2;

    [SerializeField] private int _currentMoney;
    public int currentMoney
    {
        get { return _currentMoney; }
        private set
        {
            _currentMoney = value;
            UpdateMoneyUI(); // UI 업데이트
        }
    }
    public void Update(){
        
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount; // 돈 추가
    }

    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount; // 돈 차감
            return true;
        }
        return false; // 돈이 부족하면 false 반환
    }

    public void BuyBomb()
    {
        int bombPrice = 100; // 폭탄 가격
        if (SpendMoney(bombPrice)) // 구매 성공 시
        {
            BombSystem.instance.AddBomb(2); // 폭탄 2개 추가
            Debug.Log($"폭탄 구매 완료! 현재 폭탄 개수: {BombSystem.instance.GetCurrentBombs()}");
        }
        else
        {
            Debug.Log("골드가 부족합니다.");       
        }
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $" {currentMoney}"; // UI 업데이트
            moneyText2.text = $" {currentMoney}"; // UI 업데이트
        }
    }
    public void reStartmoney()
    {
       currentMoney = 0;
    }
}
