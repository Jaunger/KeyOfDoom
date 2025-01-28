using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterOnImpact : MonoBehaviour
{
    public GameObject shatteredBallPrefab;  // Reference to the fractured ball prefab
    public AudioClip shatterSound;          // Reference to the shatter sound effect
    private BallSpawner ballSpawner;          // Reference to the ball spawner
    private AudioSource audioSource;
    public bool hasShattered = false;

    private void Start()
    {
        // Add an AudioSource component to the ball
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Find the ball spawner in the scene
        ballSpawner = FindObjectOfType<BallSpawner>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball hits a panel
        if (collision.gameObject.CompareTag("Panel") && !hasShattered)
        {
            Debug.Log("Shatter!");
            Shatter();
        }
   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !hasShattered)
        {
            Debug.Log("Player Hit");
            Shatter();

        }
    }

    void Shatter()
    {
        hasShattered = true;

        // Instantiate the shattered ball prefab at the current position and rotation
        GameObject shatteredBall = Instantiate(shatteredBallPrefab, transform.position, transform.rotation);

        // Activate the rigidbodies on the shattered pieces
        foreach (Rigidbody rb in shatteredBall.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(100, transform.position, 5);  // Optional: Add a small explosion force for effect
        }

        // Destroy the original ball
        Destroy(gameObject);

        if (ballSpawner != null)
        {
            ballSpawner.GetComponent<BallSpawner>().SpawnBall();
        }
    }
}
