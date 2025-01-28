using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int scoreValue = 10; // Score amount to add upon pickup

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(scoreValue); // Add score on item pickup
            Destroy(gameObject); // Destroy item after pickup
        }
    }
}
