using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerScript : MonoBehaviour
{
    private bool gameIsPaused;
    public GameObject controlScheme;
    public GameObject cat;
    public Button quit;
    public Text lives;
    public Text stealth;
    public Text  caught;
    public float duration;
    public GameObject player;
    public GameObject smallPlayer;


    // Start is called before the first frame update
    void Start()
    {
        controlScheme.SetActive(false);
        if(caught != null)
            caught.color = Color.clear;
        if(stealth != null)
            stealth.enabled = false;
        duration = 2f;
    }

    // Update is called once per frame
    void Update()
    {
     
       if(Input.GetKeyUp(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            pauseGame();
        }
        if (Input.GetKey(KeyCode.LeftControl) && player.gameObject.layer != 8)
        {
            if(stealth != null)
                stealth.enabled=true;
        }
        else if(stealth != null)
            stealth.enabled=false;
        if (lives != null)
            checkLives();
    }

    private void checkLives()
    {

        if (lives.text == "0")
            lose();

        if(!lives.text.Equals(player.GetComponent<FmsScript>().lives.ToString()))
        {
            Debug.Log("Caught");
            StartCoroutine(FadeOutCoroutine(duration));
            lives.text = player.GetComponent<FmsScript>().lives.ToString();
            cat.gameObject.SetActive(true);
        }

    }

    private void lose()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    public void exitGame()
    {
        Application.Quit();
    }
    private void pauseGame()
    {
        if(gameIsPaused)
        {
            controlScheme.SetActive(true);

            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<FmsScript>().isPaused = true;
            if(smallPlayer != null)
                smallPlayer.GetComponent<sfmsScriptp2>().isPaused = true;
        }
        else
        {
            controlScheme.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<FmsScript>().isPaused = false;
            if (smallPlayer != null)
                smallPlayer.GetComponent<sfmsScriptp2>().isPaused = false;


            Time.timeScale = 1;
        }
    }
    private IEnumerator FadeOutCoroutine(float duration)
    {
        float currentTime = 0f;
        Color startColor = Color.red;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Fully transparent

        while (currentTime < duration)
        {
            float t = currentTime / duration;
            caught.color = Color.Lerp(startColor, endColor, t);
            currentTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the text becomes fully transparent
        caught.color = endColor;
    }
}
