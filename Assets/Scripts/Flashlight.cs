using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject FLitem;

    public AudioSource turnOn;
    public AudioSource turnOff;
    public AudioSource pickup;

    public bool on;
    public bool off;
    public bool hasFlashLight = false;
    private bool isLookingAtFlashlight = false;
    public bool isCloseEnough = false;

    // UI Elements
    public GameObject pickupPromptUI; // Assign a UI element in the Inspector to show the "[E] to pick up flashlight" prompt

    public Transform playerCamera; // Assign the player's camera in the Inspector
    public float maxPickupDistance = 5f; // The maximum distance at which the player can pick up the flashlight

    // Optional: LayerMask to filter the raycast to specific layers
    public LayerMask interactableLayer;

    void Start()
    {
        off = true;
        flashlight.SetActive(false);
        pickupPromptUI.SetActive(false); // Make sure the prompt is hidden at the start
    }

    void Update()
    {
        CheckIfLookingAtFlashlight();
        HandlePickup();
        HandleFlashlightToggle();
    }

    private void CheckIfLookingAtFlashlight()
    {
        // Raycast from the camera to detect if the player is looking at the flashlight
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Perform the raycast with a layer mask (optional) and a max distance
        if (Physics.Raycast(ray, out hit, maxPickupDistance, interactableLayer))
        {
            //Debug.Log("Raycast hit: " + hit.collider.gameObject.name); // Debug to see what the raycast is hitting

            if (hit.collider.gameObject == FLitem)
            {
                isLookingAtFlashlight = true;
            }
            else
            {
                isLookingAtFlashlight = false;
            }
        }
        else
        {
            isLookingAtFlashlight = false;
        }

        // Show the prompt only if the player is looking at the flashlight and is close enough
        if (isLookingAtFlashlight && isCloseEnough && !hasFlashLight)
        {
            pickupPromptUI.SetActive(true);
        }
        else
        {
            pickupPromptUI.SetActive(false);
        }
    }

    private void HandlePickup()
    {
        if (isLookingAtFlashlight && isCloseEnough && !hasFlashLight && Input.GetKeyDown(KeyCode.E))
        {
            pickup.Play();
            hasFlashLight = true;
            FLitem.SetActive(false); // Disable the flashlight item in the scene
            pickupPromptUI.SetActive(false); // Hide the prompt after picking up the flashlight
        }
    }

    private void HandleFlashlightToggle()
    {
        if (hasFlashLight)
        {
            if (off && Input.GetKeyDown(KeyCode.F))
            {
                flashlight.SetActive(true);
                turnOn.Play();
                off = false;
                on = true;
            }
            else if (on && Input.GetKeyDown(KeyCode.F))
            {
                flashlight.SetActive(false);
                turnOff.Play();
                off = true;
                on = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCloseEnough = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCloseEnough = false;
        }
    }
}
