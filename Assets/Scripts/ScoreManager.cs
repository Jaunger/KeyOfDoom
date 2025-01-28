using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement; // Include this to use SceneManager

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score;

    private Text scoreText; // Reference to the UI Text component

    private string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Application.persistentDataPath + "/scoreData.dat";
            LoadScore();
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void resetScore()
    {
        score = 0;
        SaveScore();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindScoreText(); // Find the score text whenever a new scene is loaded
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
        UpdateScoreText();
        SaveScore();
    }
    public void RemoveScore(int amount) {
        if(score - amount < 0) {
            score = 0;
        } else {
            score -= amount;
        }
        Debug.Log("Score: " + score);
        UpdateScoreText();
        SaveScore();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void FindScoreText()
    {
        scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
    }

    private void SaveScore()
    {
        ScoreData data = new ScoreData();
        data.score = score;

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream file = File.Create(savePath))
        {
            formatter.Serialize(file, data);
        }
    }

    private void LoadScore()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = File.Open(savePath, FileMode.Open))
            {
                ScoreData data = (ScoreData)formatter.Deserialize(file);
                score = data.score;
            }
        }
        else
        {
            Debug.LogWarning("Save file not found; initializing score to 0.");
            score = 0;
        }
    }
}
