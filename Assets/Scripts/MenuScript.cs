using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Canvas quitMenu;

    public Button play;
    public Button quit;
    // Start is called before the first frame update
    void Start()
    {
        quitMenu.enabled = false; 
    }
    public void exitPress()
    {
        quitMenu.enabled = true;    
        play.enabled = false;
        quit.enabled = false;
    }
    public void noPress()
    {
        quitMenu.enabled = false;
        play.enabled = true;
        quit.enabled = true;
    }
    public void playLevel()
    {
        ScoreManager.Instance.resetScore();
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    public void exitGame()
    {
        Application.Quit(); 
    }
}
