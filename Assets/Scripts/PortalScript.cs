using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalScript : MonoBehaviour
{
    public Transform destination; // e destination if entering from the starting point
    public bool isUnlocked = false;           // Whether this portal has been unlocked
    public GameObject portalLight; // The light that indicates the portal is unlocked

    private void Start()
    {
        portalLight.SetActive(isUnlocked);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isUnlocked)
        { 
                // Teleport the player to the stage associated with this portal
                other.transform.position = destination.position;

            // Optionally, reset velocity or play effects here
        }
    }


    public void UnlockPortal()
    {
        isUnlocked = true;
        portalLight.SetActive(isUnlocked);

    }
}
