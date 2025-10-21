using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public static int collectedTotal = 0;

    [Header("Floating Effect")]
    public float floatAmplitude = 0.25f;
    public float floatSpeed = 2f;


    [Header("Audio")]
    public AudioClip pickupSound;      // assign in Inspector
    private AudioSource audioSource;   // internal AudioSource

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        // Create or get an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collectedTotal++;
            Debug.Log("Collected: " + collectedTotal);

            // Play sound before destroying
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
        }
    }
}
