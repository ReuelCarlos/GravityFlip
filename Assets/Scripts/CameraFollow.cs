using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Player to follow
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Camera Bounds")]
    public Vector2 minPosition;     // Bottom-left corner
    public Vector2 maxPosition;     // Top-right corner

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Clamp X and Y so camera stays within bounds
        float clampedX = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}