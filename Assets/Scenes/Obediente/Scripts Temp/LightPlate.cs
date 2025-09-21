using UnityEngine;

public class LightPlate : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other){


        if(other.gameObject.CompareTag("rLightBox") && gameObject.CompareTag("RedPlate"))
        {
            pushedRed();
        }else if(other.gameObject.CompareTag("gLightBox") && gameObject.CompareTag("GreenPlate"))
         {
            pushedGreen();
         }


    }

    void OnTriggerExit2D (Collider2D other)
    {
        if(other.gameObject.CompareTag("rLightBox") && gameObject.CompareTag("RedPlate"))
        {
            removedRed();
            
        }else if(other.gameObject.CompareTag("gLightBox") && gameObject.CompareTag("GreenPlate"))
         {
            removedGreen();
         }
    }
    void pushedRed ()
    {
        Debug.Log("PUSHED LIGHT RED");

    }
    void pushedGreen ()
    {
        Debug.Log("PUSHED LIGHT Green");

    }

    void removedRed ()
    {
        Debug.Log("LIGHT RED IS GONE");

    }
    void removedGreen ()
    {
        Debug.Log("LIGHT GREEN IS GONE");

    }
}
