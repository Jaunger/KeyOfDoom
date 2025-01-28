using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;  // The ball prefab to spawn
    public Transform spawnPoint;   // The location where the new ball will spawn

    public void SpawnBall()
    {
        Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}