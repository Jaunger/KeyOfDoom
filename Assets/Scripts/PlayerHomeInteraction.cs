using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHomeInteraction : MonoBehaviour
{
    public Text dialogueText; // Reference to the UI Text component for displaying dialogue
    public GameObject bed; // Reference to the bed object
    public float dialogueDuration = 3f; // Duration to show each line of dialogue
    public bool isNearBed = false; // Tracks if the player is near the bed

    private void Start()
    {
        dialogueText.text = ""; // Clear any initial text
        StartCoroutine(DisplayDialogue());
    }

    private void Update()
    {
        // Check for interaction if the player is near the bed
        if (isNearBed && Input.GetKeyDown(KeyCode.E))
        {
            MoveToVictoryScreen();
        }
    }

    private IEnumerator DisplayDialogue()
    {
        // Show the first line
        dialogueText.text = "Phew, I got home safely.";
        yield return new WaitForSeconds(dialogueDuration);

        // Show the second line
        dialogueText.text = "I'm feeling really tired, I should go to bed.";
        yield return new WaitForSeconds(dialogueDuration);

        // Clear the dialogue after displaying both lines
        dialogueText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered trigger");
        if (other.gameObject.CompareTag("Bed")) // Assuming the bed has the "Bed" tag
        {
            isNearBed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bed")) // Assuming the bed has the "Bed" tag
        {
            isNearBed = false;
        }
    }

    private void MoveToVictoryScreen()
    {
        // Load the victory screen scene
        SceneManager.LoadScene(4); // Replace "VictoryScene" with your scene name
    }
}
