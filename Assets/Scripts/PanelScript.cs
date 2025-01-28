using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelScript : MonoBehaviour
{
    public UnityEvent onPanelActivated;
    private bool isActivated = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isActivated)
        {
            ActivatePanel();
        }
    }

    private void ActivatePanel()
    {
        isActivated = true;
        GetComponent<Renderer>().material.color = Color.green;
        onPanelActivated.Invoke();
    }
    public void ResetPanel()
    {
        // Reset the panel color and state
        GetComponent<Renderer>().material.color = Color.white; // or the original color
        isActivated = false;
    }
}
