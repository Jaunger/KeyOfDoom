using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.Shapes;

public class PanelManager : MonoBehaviour
{
    public PanelScript[] panels;
    public UnityEvent onAllPanelsActivated;
    public DoorScript door;  // Reference to the door

    private int activatedPanelsCount = 0;

    private void Start()
    {
        foreach (PanelScript panel in panels)
        {
            panel.onPanelActivated.AddListener(OnPanelActivated);
        }
    }

    private void OnPanelActivated()
    {
        activatedPanelsCount++;
        if (activatedPanelsCount >= panels.Length)
        {
            onAllPanelsActivated.Invoke();
        }
    }
    public void ResetPanels()
    {
        foreach (PanelScript panel in panels)
        {
            panel.ResetPanel();
        }
        activatedPanelsCount = 0;
        if (door != null)
        {
            door.CloseDoor();
        }
    }


}
