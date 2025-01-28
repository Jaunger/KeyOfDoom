using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this to use UI elements

public class MiniMap : MonoBehaviour
{
    public Transform player;
    public string playerIndicatorName = "PlayerIndicator"; // Name of the UI element
    private RectTransform playerIndicator; // Reference to the UI element (dot/arrow)

    void Start()
    {
        // Find the PlayerIndicator object by name and initialize it
        GameObject indicatorObject = GameObject.Find(playerIndicatorName);
        if (indicatorObject != null)
        {
            playerIndicator = indicatorObject.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("PlayerIndicator object not found. Please ensure it is correctly named and part of the scene.");
        }
    }

    void Update()
    {
        // Update the minimap position to follow the player
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // Update the rotation of the player indicator based on the player's rotation
        if (playerIndicator != null)
        {
            playerIndicator.rotation = Quaternion.Euler(0, 0, -player.eulerAngles.y + 180);
        }
    }
}
