using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float visibleDuration = 2f;     // Duration the platform stays visible
    public float invisibleDuration = 2f;   // Duration the platform stays invisible

    private Renderer rend;                 // Reference to the platform's Renderer
    private Collider coll;                 // Reference to the platform's Collider

    void Start()
    {
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
        StartCoroutine(DisappearCycle());
    }

    IEnumerator DisappearCycle()
    {
        while (true)
        {
            // Make the platform visible and enable its collider
            rend.enabled = true;
            coll.enabled = true;
            yield return new WaitForSeconds(visibleDuration);

            // Make the platform invisible and disable its collider
            rend.enabled = false;
            coll.enabled = false;
            yield return new WaitForSeconds(invisibleDuration);
        }
    }
}
