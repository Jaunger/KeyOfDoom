using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CatScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cat;
    public GameObject portal;
    private bool playerInRange;
    private bool active = false;
    public UnityEvent onCat;
    public Text interactionText; // Reference to the UI Text component for interaction prompt
    

    void Start()
    {
        playerInRange = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            interactionText.text = "Press 'E' to place the cat";
            interactionText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                cat.SetActive(true);
                active = true;
                interactionText.gameObject.SetActive(false);
                playerInRange = false; // Reset player range after placing the cat

            }
        }
        if (active)
        {
            if (onCat != null)
            {
                portal.SetActive(true);
                onCat.Invoke();
            }
            else
            {
                Debug.Log("asd");
                portal.GetComponent<WinTerrain>().check = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = other.GetComponent<FmsScript>().hasCat;
            portal.GetComponent<WinTerrain>().check = active;

              other.GetComponent<FmsScript>().hasCat = !active;

            
        }

        if(other.gameObject.tag == "FmsScript")
        {
            playerInRange = other.GetComponent<sfmsScriptp2>().hasCat;
            Debug.Log("Cat placed" + playerInRange);

            if (Input.GetKeyDown(KeyCode.E) && playerInRange)
            { 

                    Debug.Log("Cat placed");
                    other.GetComponent<sfmsScriptp2>().hasCat = false;

          
            }
      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false; // Player is out of range
            interactionText.gameObject.SetActive(false); // Hide the interaction text
            other.GetComponent<FmsScript>().hasCat = !active;

            portal.GetComponent<WinTerrain>().check = active;

        }
        if (other.gameObject.tag == "FmsScript")
        {
            playerInRange = false; // Player is out of range
            interactionText.gameObject.SetActive(false); // Hide the interaction text
            other.GetComponent<sfmsScriptp2>().hasCat = !active;

        }
    }
}
