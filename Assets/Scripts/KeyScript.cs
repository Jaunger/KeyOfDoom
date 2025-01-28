using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyScript : MonoBehaviour
{
    Animator animator;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FmsScript")
        {
            AudioSource.PlayClipAtPoint(clip, other.transform.position);
            Destroy(gameObject);
        }
    }
}
