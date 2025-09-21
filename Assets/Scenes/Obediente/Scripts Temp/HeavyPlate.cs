using UnityEngine;

public class HeavyPlate : MonoBehaviour
{


    void OnTriggerEnter2D (Collider2D other){


        if(other.gameObject.CompareTag("rHeavyBox") && gameObject.CompareTag("RedPlate"))
        {
            pushedRed();
        }else if(other.gameObject.CompareTag("gHeavyBox") && gameObject.CompareTag("GreenPlate"))
         {
            pushedGreen();
         }


    }

    void OnTriggerExit2D (Collider2D other)
    {
        if(other.gameObject.CompareTag("rHeavyBox") && gameObject.CompareTag("RedPlate"))
        {
            removedRed();
            
        }else if(other.gameObject.CompareTag("gHeavyBox") && gameObject.CompareTag("GreenPlate"))
         {
            removedGreen();
         }
    }
    void pushedRed ()
    {
        Debug.Log("PUSHED HEAVY RED");

    }
    void pushedGreen ()
    {
        Debug.Log("PUSHED HEAVY Green");

    }

    void removedRed ()
    {
        Debug.Log("HEAVY RED IS GONE");

    }
    void removedGreen ()
    {
        Debug.Log("HEAVY GREEN IS GONE");

    }

   
}
