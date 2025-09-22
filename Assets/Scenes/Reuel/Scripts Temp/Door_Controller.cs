using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    public GameObject[] plates;
    public GameObject door;
    private bool doorOpened = false;

    void Update()
    {
        if (doorOpened) return;

        bool allPressed = true;

        foreach (GameObject plateObj in plates)
        {
            bool pressed = false;

            // Check if the plate has LightPlate or HeavyPlate component
            LightPlate light = plateObj.GetComponent<LightPlate>();
            HeavyPlate heavy = plateObj.GetComponent<HeavyPlate>();

            if (light != null)
                pressed = light.isPressed;
            else if (heavy != null)
                pressed = heavy.isPressed;

            Debug.Log(plateObj.name + " pressed: " + pressed);

            if (!pressed)
            {
                allPressed = false;
            }
        }

        if (allPressed)
        {
            doorOpened = true;
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        Debug.Log("All plates pressed! Door is opening!");
        door.SetActive(false); // Or trigger your animation here
    }
}
