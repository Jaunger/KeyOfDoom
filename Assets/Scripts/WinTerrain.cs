using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTerrain : MonoBehaviour
{
    public bool check;
    public GameObject bloom;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckComplete();
    }

    private void CheckComplete()
    {
        if (check)
            bloom.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (check && other.gameObject.tag == "Player")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(2);
        }
    }
}
