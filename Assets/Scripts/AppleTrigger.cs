using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTrigger : MonoBehaviour
{
    public GameObject Sphere;
    public AudioSource applefalling;
    private bool played = false;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FmsScript")
        {
            Sphere.GetComponent<Rigidbody>().useGravity = true;
            if (!played)
            {
                applefalling.Play();
                played = true;  
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
