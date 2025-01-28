using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour

{
    public Text txt;
    public float fadeSpeed = 5;
    public bool enterance = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        ColorChange();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "FmsScript") enterance = true;
        if (col.gameObject.tag == "Player") enterance = true;
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "FmsScript") enterance = false;
        if (col.gameObject.tag == "Player") enterance = false;
    }
    private void ColorChange()
    {
        if (enterance)
            txt.color = Color.Lerp(txt.color, Color.white, fadeSpeed * Time.deltaTime);
        if (!enterance)
            txt.color = Color.Lerp(txt.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
}
