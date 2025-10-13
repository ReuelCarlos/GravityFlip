using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PushAway : MonoBehaviour
{
    [Header("Push Settings")]
    public float pushForce = 10f; // strength of the push
    public float maxDistance = 5f; // optional range scaling

    private void Reset()
    {
        // Ensure trigger is enabled
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        // Calculate direction from center to object
        Vector2 direction = (other.transform.position - transform.position);
        float distance = direction.magnitude;

        if (distance > 0.01f)
        {
            direction.Normalize();

            // Optional: stronger push when closer
            float strength = pushForce;
            if (maxDistance > 0f)
                strength *= Mathf.Clamp01(1f - (distance / maxDistance));

            rb.AddForce(direction * strength, ForceMode2D.Force);
        }
    }
}
