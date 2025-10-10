using UnityEngine;

public class PCReuel : MonoBehaviour
{
    public float moveSpeed = 8f;

    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    public Animator animator;

    
    private GameObject box;
    public float distance = 1f;
    public LayerMask boxMask;

    GameObject box1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.right, distance, boxMask);
      
        if (
            hit1.collider != null &&
            (
                hit1.collider.gameObject.tag == "rHeavyBox" ||
                hit1.collider.gameObject.tag == "rLightBox" ||
                hit1.collider.gameObject.tag == "gHeavyBox" ||
                hit1.collider.gameObject.tag == "gLightBox"
            )
            && Input.GetKeyDown(KeyCode.E))
        {
            box1 = hit1.collider.gameObject;
            box1.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            box1.GetComponent<FixedJoint2D>().enabled = true;
            box1.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            
        }else if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("Released");
                box1.GetComponent<FixedJoint2D>().enabled = false;
                box1.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
                
                
            }
        

        // Player movement
        float inputX = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(inputX));

        if (inputX != 0)
        {
            bool flippers = inputX < 0;
            transform.rotation = Quaternion.Euler(new Vector3(0f, flippers ? 180f : 0f, 0f));
        }

        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);

        // Ground check
        Vector2 checkDirection = Physics2D.gravity.normalized;
        Vector2 origin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, checkDirection, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("Spikers"))
        {
            Debug.Log("MEW");
        }
    }
 
}
