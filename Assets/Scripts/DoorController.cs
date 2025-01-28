using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f;  // Angle to open the door
    public float openSpeed = 2f;   // Speed at which the door opens
    private bool isOpening = false;
    private Quaternion initialRotation;
    private Quaternion openRotation;

    void Start()
    {
        initialRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * initialRotation;
    }

    void Update()
    {
        if (isOpening)
        {
            // Smoothly rotate the door to the open position
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
    }
}
