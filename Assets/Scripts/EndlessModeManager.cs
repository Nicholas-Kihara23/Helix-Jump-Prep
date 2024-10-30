using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndlessModeManager : MonoBehaviour
{
    private int score;
    public TMP_Text scoreText;

    void Start()
    {
        score = 0;
        UpdateScoreUI();
    }

    void Update()
    {
        // Game keeps running indefinitely; no GameOver logic
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void ExitMode()
    {
        Debug.Log("Exiting Endless Mode");
        // Transition back to Main Menu or Mode Selection screen
    }
}

