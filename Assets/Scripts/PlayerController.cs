using UnityEngine;

public class PlayerControllers : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    public Transform groundCheckBottom;
    public Transform groundCheckTop;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float lastGroundedTime;
    private float lastJumpPressedTime;
    private float moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        CheckGrounded();

        if (isGrounded)
            lastGroundedTime = Time.time;

        if (Input.GetButtonDown("Jump"))
            lastJumpPressedTime = Time.time;

        if (lastGroundedTime + coyoteTime > Time.time &&
            lastJumpPressedTime + jumpBufferTime > Time.time)
        {
            Jump();
            lastJumpPressedTime = -999f;
            lastGroundedTime = -999f;
        }

        if (Input.GetKeyDown(KeyCode.F))
            FlipGravity();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void CheckGrounded()
    {
        if (Physics2D.gravity.y < 0)
            isGrounded = Physics2D.OverlapCircle(groundCheckBottom.position, groundCheckRadius, groundLayer);
        else
            isGrounded = Physics2D.OverlapCircle(groundCheckTop.position, groundCheckRadius, groundLayer);
    }

    void Jump()
    {
        Vector2 jumpDir = -Physics2D.gravity.normalized;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.linearVelocity += jumpDir * jumpForce;
    }


    void FlipGravity()
    {
        Physics2D.gravity *= -1;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }
}
