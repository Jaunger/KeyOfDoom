using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    public Text ans;
    private string answer = "3859";  // The correct code
    private string enteredCode = ""; // Store the entered code
    public DoorController door;      // Reference to the DoorController

    void Start()
    {
        ans.text = "";  // Clear the display at the start
    }

    public void Number(int number)
    {
        if (enteredCode.Length < 4)  // Limit the input to 4 digits
        {
            enteredCode += number.ToString();
            ans.text = enteredCode;
        }
    }

    public void Execute()
    {
        if (enteredCode == answer)
        {
            Debug.Log("Correct Code. Door Opening...");
            door.OpenDoor();  // Call the door's OpenDoor method
        }
        else
        {
            Debug.Log("Wrong Answer");
            ans.text = "INCORRECT";
            StartCoroutine(ClearDisplay());
        }
    }

    private IEnumerator ClearDisplay()
    {
        yield return new WaitForSeconds(1.5f);  // Wait 1.5 seconds
        enteredCode = "";  // Reset the entered code
        ans.text = "";  // Clear the display
    }

    public void Clear()
    {
        enteredCode = "";  // Reset the entered code
        ans.text = "";  // Clear the display
    }
}
