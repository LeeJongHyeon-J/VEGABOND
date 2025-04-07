using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 50f;
    public float deceleration = 50f;
    public float velocityPower = 0.9f;
    public float frictionAmount = 0.2f;

    [Header("Jump")]
    public float jumpForce = 10f;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool canDash = true;
    private bool isDashing;
    private float dashTimeLeft;
    private float lastDashTime;
    private float moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (groundCheck == null)
        {
            groundCheck = transform;
        }
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        CheckGround();
        UpdateTimers();

        if (!isDashing)
        {
            HandleJump();
            HandleDash();
        }

        // 애니메이션 업데이트
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            HandleMovement();
        }
    }

    private void UpdateTimers()
    {
        if (!canDash && Time.time >= lastDashTime + dashCooldown)
        {
            canDash = true;
        }

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void HandleMovement()
    {
        float targetSpeed = moveInput * moveSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velocityPower) * Mathf.Sign(speedDiff);
        
        rb.AddForce(movement * Vector2.right);

        if (isGrounded && Mathf.Abs(moveInput) < 0.01f)
        {
            float frictionForce = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            frictionForce *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -frictionForce, ForceMode2D.Impulse);
        }

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        canDash = false;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;

        float dashDirection = transform.localScale.x;
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);
    }

    private void UpdateAnimations()
{
    // 속도 애니메이션
    animator.SetFloat("Speed", Mathf.Abs(moveInput));

    // 점프 애니메이션
    animator.SetBool("IsJumping", !isGrounded && rb.velocity.y > 0);
    animator.SetBool("IsFalling", !isGrounded && rb.velocity.y < 0);
    animator.SetBool("IsGrounded", isGrounded);

    // 제자리 점프와 러닝 점프 구분
    if (!isGrounded && rb.velocity.y > 0)
    {
        float horizontalSpeed = Mathf.Abs(rb.velocity.x); // 현재 X축 속도
        float threshold = 0.1f; // 제자리와 러닝 구분 임계값
        if (horizontalSpeed < threshold)
        {
            animator.Play("IdleJump"); // 제자리 점프
        }
        else
        {
            animator.Play("RunJump"); // 러닝 점프
        }
    }

    // 대시 애니메이션
    animator.SetBool("IsDashing", isDashing);
}


    // 벽 충돌 방지를 위한 콜라이더 조정 메서드 (선택적)
    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            // 약간의 힘을 가해 벽 끼임 방지
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(pushDirection * 0.1f, ForceMode2D.Impulse);
        }
    }
}