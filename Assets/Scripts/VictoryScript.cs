using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    public Button retry;
    public Button quit;
    public AudioClip victory;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if(victory != null)
            AudioSource.PlayClipAtPoint(victory, Camera.main.transform.position);

    }
    public void playLevel()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    public void exitGame()
    {
        Application.Quit();
    }

}
