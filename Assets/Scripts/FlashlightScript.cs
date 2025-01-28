using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    public GameObject Flashlight;
    private bool FlashlightActive = false;
    public bool hasFlashLigh = false;
    public bool nearFlashLight = false;
    // Start is called before the first frame update
    void Start()
    {
        Flashlight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFlashLigh)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Flashlight.gameObject.SetActive(!FlashlightActive);
                FlashlightActive = !FlashlightActive;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && nearFlashLight)
        {
            hasFlashLigh = true;
      
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cat")
        {
            nearFlashLight = true;
        }
    }
}
