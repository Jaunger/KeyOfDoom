using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatScript : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2f;

    private bool movingToB = true;
    private Vector3 lastPosition;
    private GameObject player;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // Move the platform between pointA and pointB
        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
            if (transform.position == pointB) movingToB = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
            if (transform.position == pointA) movingToB = true;
        }

        // Calculate the platform's movement since the last frame
        Vector3 platformMovement = transform.position - lastPosition;

        // Move the player by the same amount
        if (player != null)
        {
            player.transform.position += platformMovement;
        }

        // Update the last position for the next frame
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }

}
