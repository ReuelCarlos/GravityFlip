using UnityEngine;

public class FreezeTimer : MonoBehaviour
{
    [SerializeField] private WinLose winLoseScript;

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winLoseScript.SetTimerEnabled(false);
            Debug.Log("time stop here");
        }
    }

     void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winLoseScript.SetTimerEnabled(true);
        }
    }
}
