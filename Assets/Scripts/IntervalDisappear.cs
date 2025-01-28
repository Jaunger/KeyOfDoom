using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalDisappear : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;

    public float intervalDuration = 2f;

    private Renderer rend1;
    private Renderer rend2;
    private Renderer rend3;
    private Collider coll1;
    private Collider coll2;
    private Collider coll3;

    void Start()
    {
        rend1 = platform1.GetComponent<Renderer>();
        rend2 = platform2.GetComponent<Renderer>();
        rend3 = platform3.GetComponent<Renderer>();
        coll1 = platform1.GetComponent<Collider>();
        coll2 = platform2.GetComponent<Collider>();
        coll3 = platform3.GetComponent<Collider>();

        StartCoroutine(PlatformCycle());
    }

    IEnumerator PlatformCycle()
    {
        while (true)
        {
            // Show platform 1 and 2, hide platform 3
            rend1.enabled = true;
            coll1.enabled = true;
            rend2.enabled = true;
            coll2.enabled = true;
            rend3.enabled = false;
            coll3.enabled = false;
            yield return new WaitForSeconds(intervalDuration);

            // Hide platform 1 and 2, show platform 3
            rend1.enabled = false;
            coll1.enabled = false;
            rend2.enabled = false;
            coll2.enabled = false;
            rend3.enabled = true;
            coll3.enabled = true;
            yield return new WaitForSeconds(intervalDuration);
        }
    }
}
