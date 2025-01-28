using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DoorScript : MonoBehaviour
{
    public Animator animator;
    public GameObject playerRef;


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "FmsScript" && playerRef.GetComponent<sfmsScriptp2>().hasKey == true)
        {
            Debug.Log("Door Opened");
            animator.SetBool("isOpen", true);
        }
    }

    private void doorControl(string state)
    {
        animator.SetTrigger(state);
    }
    public void CloseDoor()
    {
        animator.SetBool("isOpen", false);
    }
    public void OpenDoor()
    {
        Debug.Log("Door Opened123");
        animator.SetBool("isOpen", true);
    }

}
