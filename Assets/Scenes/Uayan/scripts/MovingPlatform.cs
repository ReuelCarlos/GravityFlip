using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Start position
    public Transform pointB; // End position
    public float speed = 2f;

    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        // Move platform towards target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Swap target when reaching one point
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    // Attach any object standing on the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Make the colliding object a child of the platform so it moves with it
        collision.collider.transform.SetParent(transform);
    }

    // Detach when it leaves
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Remove the parent so it moves independently again
        collision.collider.transform.SetParent(null);
    }
}
