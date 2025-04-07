using UnityEngine;
using TMPro;
using UnityEngine.UI; // TextMeshPro 사용을 위한 네임스페이스

public class BombSystem : MonoBehaviour 
{
    public static BombSystem instance;
    public GameObject bombPrefab;
    public float throwForce = 5f;
    public float upwardForce = 2f;

    [SerializeField] private int currentBombs;
    [SerializeField] private Text bombCountText; // 폭탄 개수 표시할 UI 텍스트

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
        UpdateBombUI(); // 초기 UI 업데이트
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowBomb();
        }
    }

    public void ThrowBomb()
    {
        if (currentBombs > 0 && bombPrefab != null)
        {
            float direction = transform.localScale.x >= 0 ? 1f : -1f;
            Vector3 throwPosition = transform.position + transform.right * 0.5f * direction;

            GameObject bomb = Instantiate(bombPrefab, throwPosition, Quaternion.identity);
            Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
            if (bombRb != null)
            {
                Vector2 throwDirection = new Vector2(direction, upwardForce);
                bombRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }

            currentBombs--;
            UpdateBombUI(); // 폭탄 사용 후 UI 업데이트
        }
    }

    public void AddBomb(int amount)
    {
        if (MoneyManager.instance.currentMoney >= 100)
        {
            MoneyManager.instance.SpendMoney(100);
            currentBombs += amount;
            UpdateBombUI(); // 폭탄 추가 후 UI 업데이트
        }
        else
        {
            Debug.Log("Not enough money to heal!");
        }
    }

    public int GetCurrentBombs()
    {
        return currentBombs;
    }

    private void UpdateBombUI()
    {
        if (bombCountText != null)
        {
            bombCountText.text = $"{currentBombs}";
        }
    }
      public void reStartbomb()
    {
       currentBombs =5;
    }
}