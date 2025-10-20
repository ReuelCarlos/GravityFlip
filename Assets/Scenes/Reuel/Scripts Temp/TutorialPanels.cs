using UnityEngine;

public class TutorialPanels : MonoBehaviour
{
    public GameObject Panel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Panel.SetActive(false);
    }

    void OnCollisionEnter2D( Collision2D other){

        if(other.gameObject.CompareTag("Player")){
            Panel.SetActive(true);
        }
    }

    void OnCollisionExit2D( Collision2D other){

        if(other.gameObject.CompareTag("Player")){
            Panel.SetActive(false);
        }
    }
}
