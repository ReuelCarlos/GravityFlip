using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject[] plates;

    public Transform leftDoor;
    public Transform rightDoor;

    public float slideDistance = 2f;
    public float slideSpeed = 5f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;

    void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;

        leftOpenPos = leftClosedPos + Vector3.left * slideDistance;
        rightOpenPos = rightClosedPos + Vector3.right * slideDistance;
    }

    void Update()
    {
        bool allPressed = true;

        foreach (GameObject plateObj in plates)
        {
            bool pressed = false;
            LightPlate light = plateObj.GetComponent<LightPlate>();
            HeavyPlate heavy = plateObj.GetComponent<HeavyPlate>();

            if (light != null) pressed = light.isPressed;
            else if (heavy != null) pressed = heavy.isPressed;

            if (!pressed) allPressed = false;
        }

        if (allPressed)
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftOpenPos, Time.deltaTime * slideSpeed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightOpenPos, Time.deltaTime * slideSpeed);
        }
        else
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftClosedPos, Time.deltaTime * slideSpeed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightClosedPos, Time.deltaTime * slideSpeed);
        }
    }
}
