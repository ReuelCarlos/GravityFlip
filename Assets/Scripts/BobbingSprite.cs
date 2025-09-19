using UnityEngine;

public class BobbingSprite : MonoBehaviour
{
    public float amplitude = 10f;   // how far up/down it moves
    public float frequency = 2f;    // how fast it moves
    public float phaseOffset = 0f;  // lets each sprite start at a different point

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin((Time.time + phaseOffset) * frequency) * amplitude;
        transform.localPosition = startPos + new Vector3(0, y, 0);
    }
}
