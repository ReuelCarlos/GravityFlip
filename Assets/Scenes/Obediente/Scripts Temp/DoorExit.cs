using UnityEngine;

public class DoorExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorController door = FindObjectOfType<DoorController>();
            if (door != null && door.IsOpen())
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Editor
#endif
            }
        }
    }
}
