using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 300f;
    private Animator animator;
    private RectTransform rectTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // ������ �Ǵ� ���� ����Ű �Է� �ޱ�
        float moveInput = Input.GetAxisRaw("Horizontal");

        // �̵� ���� Ȯ��
        bool IsWalking = moveInput != 0;
        animator.SetBool("IsWalking", IsWalking);

        

        // �̵� ó��
        if (IsWalking)
        {
            // ���� ��ġ�� �������� �̵� ���
            Vector2 newPosition = rectTransform.anchoredPosition + new Vector2(moveInput * speed * Time.deltaTime, 0);
            rectTransform.anchoredPosition = newPosition;

            // ������/���ʿ� ���� ĳ���� ���� ��ȯ
            if (moveInput > 0)
                rectTransform.localScale = new Vector3(1, 1, 1);
            else if (moveInput < 0)
                rectTransform.localScale = new Vector3(-1, 1, 1);
        }
        

    }
    void Awake()
    {
        // �� ��ȯ�ص� ������Ʈ ����
        DontDestroyOnLoad(this.gameObject);
    }


}