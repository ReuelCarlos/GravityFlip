using UnityEngine;

public class PCReuel : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 15f;
    public float airControlFactor = 0.6f;

    public float jumpForce = 12f;
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float targetSpeed;
    private float currentSpeed;

    public Animator animator;

    private bool boxInRange = false;
    private GameObject box;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (boxInRange && Input.GetKey(KeyCode.E))
        {
            box.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        targetSpeed = inputX * moveSpeed;

        animator.SetFloat("Speed", Mathf.Abs(inputX));

        if (targetSpeed != 0)
        {
            bool flippers = targetSpeed < 0;
            transform.rotation = Quaternion.Euler(new Vector3(0f, flippers ? 180f : 0f, 0f));
        }

        float accel = isGrounded ? acceleration : acceleration * airControlFactor;
        float decel = isGrounded ? deceleration : deceleration * airControlFactor;

        if (Mathf.Abs(inputX) > 0.01f)
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accel * Time.deltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decel * Time.deltaTime);

        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);

        Vector2 checkDirection = Physics2D.gravity.normalized;
        Vector2 origin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, checkDirection, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            Vector2 jumpDir = -Physics2D.gravity.normalized;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.linearVelocity += jumpDir * jumpForce;

            jumpBufferCounter = 0;
        }

        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && !isGrounded)
        {
            if (Vector2.Dot(rb.linearVelocity, -Physics2D.gravity.normalized) > 0)
            {
                rb.linearVelocity *= 0.5f;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 checkDirection = Physics2D.gravity.normalized;
        Vector2 origin = (Vector2)transform.position;
        Gizmos.DrawLine(origin, origin + checkDirection * groundCheckDistance);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("rHeavyBox") || other.gameObject.CompareTag("gHeavyBox") || other.gameObject.CompareTag("gLightBox") || other.gameObject.CompareTag("rLightBox"))
        {
            box = other.gameObject;
            boxInRange = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("rHeavyBox") || other.gameObject.CompareTag("gHeavyBox") || other.gameObject.CompareTag("gLightBox") || other.gameObject.CompareTag("rLightBox"))
        {
            boxInRange = false;
            other.gameObject.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
