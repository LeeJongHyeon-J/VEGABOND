using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;

    private readonly string ATTACK = "Attack";
    private readonly string CROUCH = "Crouch";
    private readonly string DASH = "Dash";
    private readonly string DEATH = "Death";
    private readonly string EDGE = "Edge";
    private readonly string FALL = "Fall";
    private readonly string HURT = "Hurt";
    private readonly string IDLE = "Idle";
    private readonly string JUMP = "Jump";
    private readonly string LADDER = "Ladder";
    private readonly string RUN = "Run";
    private readonly string SLIDE = "Slide";
    private readonly string WALK = "Walk";

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public float dashSpeed = 20f;
    public float slideSpeed = 8f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Movement input
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Basic movement
        HandleMovement(horizontalInput);
        
        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // Crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouch();
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }

        // Check if falling
        if (rb.velocity.y < -0.1f && !isGrounded)
        {
            Fall();
        }

        // Idle state when no movement
        if (Mathf.Abs(horizontalInput) < 0.1f && isGrounded)
        {
            Idle();
        }
    }

    private void HandleMovement(float horizontalInput)
    {
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
        }

        // Set movement speed using AddForce instead of directly setting velocity
        rb.AddForce(new Vector2(horizontalInput * moveSpeed, 0f), ForceMode2D.Force);

        // Animate walking/running
        if (Mathf.Abs(horizontalInput) > 0.1f && isGrounded)
        {
            if (Mathf.Abs(horizontalInput) > 0.5f)
            {
                Run();
            }
            else
            {
                Walk();
            }
        }
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK);
    }

    public void Crouch()
    {
        animator.SetBool(CROUCH, true);
    }

    public void StopCrouch()
    {
        animator.SetBool(CROUCH, false);
    }

    public void Dash()
    {
        animator.SetTrigger(DASH);
        float direction = transform.localScale.x;
        rb.velocity = new Vector2(direction * dashSpeed, rb.velocity.y);
    }

    public void Fall()
    {
        animator.SetBool(FALL, true);
    }

    public void Idle()
    {
        animator.SetTrigger(IDLE);
        animator.SetBool(RUN, false);
        animator.SetBool(WALK, false);
    }

    public void Jump()
    {
        animator.SetTrigger(JUMP);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Run()
    {
        animator.SetBool(RUN, true);
        animator.SetBool(WALK, false);
        animator.ResetTrigger(IDLE);
    }

    public void Walk()
    {
        animator.SetBool(WALK, true);
        animator.SetBool(RUN, false);
        animator.ResetTrigger(IDLE);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
