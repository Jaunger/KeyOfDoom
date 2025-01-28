using UnityEngine;

public class LavaHazard : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
            ScoreManager.Instance.RemoveScore(15); // Subtract score on hazard collision
            // Optional: Reset the player's velocity if needed
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}
