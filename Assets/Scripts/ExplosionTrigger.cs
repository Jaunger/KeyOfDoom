using System.Collections;
using UnityEngine;
using UnityEngine.UI; // For displaying dialogue

public class CatExplosion : MonoBehaviour
{
    public GameObject fracturedCat; // The parent GameObject containing all cat fragments
    public float explosionForce = 500f; // Force of the explosion
    public float explosionRadius = 5f; // Radius of the explosion
    public Text dialogueText; // Reference to UI Text component for dialogue

    private bool hasExploded = false; // Ensure the explosion happens only once

    private void Start()
    {

        // Disable gravity and make kinematic for all fragments at start
        foreach (Transform fragment in fracturedCat.transform)
        {
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Disable physics simulation
                rb.useGravity = false; // Disable gravity
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasExploded)
        {
            ExplodeCat();
            ShowDialogue();
            hasExploded = true; // Prevents further explosions
        }
    }

    private void ExplodeCat()
    {
        Vector3 explosionOrigin = fracturedCat.transform.position; // Use the cat's position as the explosion origin

        // Apply explosion force to each fragment of the cat
        foreach (Transform fragment in fracturedCat.transform)
        {
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Enable physics simulation
                rb.useGravity = true;   // Enable gravity
                rb.AddExplosionForce(explosionForce, explosionOrigin, explosionRadius);
            }
        }
    }

    private void ShowDialogue()
    {
        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        // Show dialogue on the screen
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "Holy, I have to fix that statue!";
        yield return new WaitForSeconds(3f); // Duration of dialogue display
        dialogueText.text = ""; // Clear the dialogue text
        dialogueText.gameObject.SetActive(false);

    }
}
