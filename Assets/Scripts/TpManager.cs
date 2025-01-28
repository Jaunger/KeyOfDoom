using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpManager : MonoBehaviour
{
    public GameObject player;
    public GameObject bigPlayer;
    public Transform playerPlace;
    public Transform bigPlayerPlace;

    public bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger");
        if (isActive)
        {
            if (other.gameObject.tag == "FmsScript")
            {
                Debug.Log("Player entered the trigger");

                player.SetActive(false);
                bigPlayer.SetActive(true);
                bigPlayer.transform.position = bigPlayerPlace.position;

            }
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Player entered the trigger");

                player.SetActive(true);
                bigPlayer.SetActive(false);
                player.transform.position = playerPlace.position;


            }
        }
    }
    public void UnlockPortal()
    {
        isActive = true;
    }
}
