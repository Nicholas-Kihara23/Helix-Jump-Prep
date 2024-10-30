using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ZenModeManager : MonoBehaviour
{
    private int score;
    public TMP_Text scoreText;

    void Start()
    {
        score = 0;
        UpdateScoreUI();
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
        Debug.Log("Exiting Zen Mode");
        // Return to the Main Menu or Mode Selection screen
    }
}

