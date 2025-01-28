using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public Text finalScoreText; // Reference to the UI Text component

    private void Start()
    {
        // Ensure ScoreManager instance exists
        if (ScoreManager.Instance != null)
        {
            int finalScore = ScoreManager.Instance.score; // Get the score from ScoreManager
            finalScoreText.text = "Your Score: " + finalScore.ToString(); // Display the score
        }
        else
        {
            finalScoreText.text = "Your Score: 0"; // Fallback if ScoreManager doesn't exist
        }
    }
}
