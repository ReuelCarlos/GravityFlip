using UnityEngine;

public class ObstacleLooper : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform[] points; // Add multiple points here in Inspector
    public float speed = 2f;
    public float rotationSpeed = 5f; // How fast it rotates toward target

    private int currentPointIndex = 0;

    void Start()
    {
        if (points.Length > 0)
            transform.position = points[0].position;
    }

    void Update()
    {
        if (points.Length < 2) return;

        Transform targetPoint = points[currentPointIndex];

        // Move towards position
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Smoothly rotate toward the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetPoint.rotation, rotationSpeed * Time.deltaTime);

        // Check if reached the current point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Attach any object standing on it
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Detach when leaving
        collision.collider.transform.SetParent(null);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Push any object with a Rigidbody2D
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
            rb.AddForce(pushDirection * speed * 50f * Time.deltaTime, ForceMode2D.Force);
        }
    }
}
