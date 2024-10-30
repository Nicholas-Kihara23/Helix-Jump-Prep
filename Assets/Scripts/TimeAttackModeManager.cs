using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TimeAttackModeManager : MonoBehaviour
{
    public int stageTimeLimit = 60; // Total time per stage in seconds
    public int additionalTime = 10; // Time added when collecting a pickup
    private float timeRemaining;
    private bool isTimeRunning = true;

    public TMP_Text timerText;
    public TMP_Text scoreText;

    private int score;

    void Start()
    {
        timeRemaining = stageTimeLimit;
        score = 0;
        UpdateUI();
    }

    void Update()
    {
        if (isTimeRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                GameOver();
            }

            UpdateUI();
        }
    }

    public void CollectTimePickup()
    {
        timeRemaining += additionalTime;
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();

        UpdateUI();
    }

    private void UpdateUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
        scoreText.text = "Score: " + score.ToString();
    }

    private void GameOver()
    {
        isTimeRunning = false;
        Debug.Log("Time Attack Mode Game Over");
        // Trigger Game Over screen
    }
}

