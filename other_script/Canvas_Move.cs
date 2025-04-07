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
        // 오른쪽 또는 왼쪽 방향키 입력 받기
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 이동 상태 확인
        bool IsWalking = moveInput != 0;
        animator.SetBool("IsWalking", IsWalking);

        

        // 이동 처리
        if (IsWalking)
        {
            // 현재 위치를 기준으로 이동 계산
            Vector2 newPosition = rectTransform.anchoredPosition + new Vector2(moveInput * speed * Time.deltaTime, 0);
            rectTransform.anchoredPosition = newPosition;

            // 오른쪽/왼쪽에 따라 캐릭터 방향 전환
            if (moveInput > 0)
                rectTransform.localScale = new Vector3(1, 1, 1);
            else if (moveInput < 0)
                rectTransform.localScale = new Vector3(-1, 1, 1);
        }
        

    }
    void Awake()
    {
        // 씬 전환해도 오브젝트 유지
        DontDestroyOnLoad(this.gameObject);
    }


}