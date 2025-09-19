using UnityEngine;

public class LogoBob : MonoBehaviour
{
    public float amplitude = 10f;  // movement height
    public float frequency = 1.5f; // speed of bobbing
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startPos + new Vector3(0, y, 0);
    }
}
