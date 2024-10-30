using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using System.Xml.Linq;


public class GameManagerX : MonoBehaviour
{
    public int bestScore;
    public int currentScore;
    public int currentStage = 0;


    public string gameId = "5721134"; // Replace with your Unity Game ID
    public bool testMode = true;
    public static GameManagerX singleton;

    // New properties for game modes
    public enum GameMode 
    {   Classic, 
        TimeAttack, 
        Endless, 
        Zen 
    }

    public GameMode currentMode;

    public int timeRemaining = 60; // Only used in Time Attack mode
    private bool isPaused = false;
    private bool isGameOver = false;

    public GameObject mainMenuPanel;
    public GameObject modeSelectionPanel;
    public GameObject pauseMenuPanel;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI textBest;

    private UIManagerX uiManager;
    private AdListener adListener;
    private AdInitializationListener adInitializationListener;

    public Leaderboard leaderboard;

    void Awake()
    {
        Advertisement.Initialize("5721134", true); // Initialize ads once
        
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager initialized.");

            adInitializationListener = new AdInitializationListener();
            Advertisement.Initialize(gameId, testMode, adInitializationListener);

            bestScore = PlayerPrefs.GetInt("Highscore", 0); // Default to 0 if not set
            currentScore = 0; // Initialize currentScore
        }
        else if (singleton != this) 
        {
            Debug.LogWarning("Another instance of GameManager exists!");
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("Highscore", 0);
        // Initialize AdListener
        adListener = gameObject.AddComponent<AdListener>();
        adListener.LoadAd("Interstitial_Android"); // Load the ad at startup

        // Find and assign Leaderboard script if it is not manually assigned
        if (leaderboard == null)
        {
            leaderboard = FindObjectOfType<Leaderboard>();
        }
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManagerX>();
        adListener = gameObject.AddComponent<AdListener>();
        adListener.LoadAd("Interstitial_Android"); // Load the ad at startup

        ShowMainMenu();
        Debug.Log("AdListener added.");
    }
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        modeSelectionPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }
    public void ShowModeSelection()
    {
        mainMenuPanel.SetActive(false);
        modeSelectionPanel.SetActive(true);
    }
    public void StartGame(GameMode mode)
    {
        currentMode = mode;
        currentScore = 0;
        timeRemaining = 60; // Example start time for Time Attack
        isGameOver = false;

        // Hide all menus
        mainMenuPanel.SetActive(false);
        modeSelectionPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);

        switch (mode)
        {
            case GameMode.Classic:
                StartClassicMode();
                break;
            case GameMode.TimeAttack:
                StartCoroutine(TimeAttackCountdown());
                break;
            case GameMode.Endless:
                // No additional setup for endless
                break;
            case GameMode.Zen:
                // No score or time; purely for relaxation
                break;
            

        }
    }
    private void StartClassicMode()
    {
        currentStage = 0;
        NextLevel();
    }
    private System.Collections.IEnumerator TimeAttackCountdown()
    {
        while (timeRemaining > 0 && currentMode == GameMode.TimeAttack && !isGameOver)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            UpdateTimeUI();

            if (timeRemaining <= 0)
            {
                TriggerGameOver();
                
            }

            // Update the UI time display
            uiManager.UpdateTimeDisplay(timeRemaining);
        }
    }

    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<BallControllerX>().ResetBall();
        FindObjectOfType<HelixControllerX>().LoadStage(currentStage);
        Debug.Log("Next level Called");
    }

    public void RestartLevel()
    {
        Debug.Log("Game Over");

        // Show the ad before restarting the level
        adListener.LoadAd("Interstitial_Android"); // Ensure the ad is loaded
       
        singleton.currentScore = 0;
        FindObjectOfType<BallControllerX>().ResetBall();
        FindObjectOfType<HelixControllerX>().LoadStage(currentStage);
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        Debug.Log("Score added: " + scoreToAdd + ", Current Score: " + currentScore);

        // Notify UIManagerX to update the score display
        uiManager = FindObjectOfType<UIManagerX>();

        if (uiManager != null)
        {
            uiManager.UpdateScore();
            Debug.Log("Score UI updated.");
        }
        else
        {
            Debug.LogError("UIManagerX not found!");
        }

        Debug.Log("Current Score after adding: " + currentScore);

        // Save and change the best score from the currentScore
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("Highscore", currentScore);
            PlayerPrefs.Save();

            // Update leaderboard with new best score
            if (leaderboard != null)
            {
                leaderboard.AddScore(bestScore);
            }
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenuPanel.SetActive(isPaused);
        uiManager.TogglePauseScreen(isPaused);
    }
    public void TriggerGameOver()
    {
        isGameOver = true;
        StopGame();
        ShowAd();
    }
    private void StopGame()
    {
        Time.timeScale = 0;
    }
    public void ShowAd()
    {
        adListener.LoadAd("Interstitial_Android"); // Load and automatically show ad
    }
    
    private void HandleAdResult(ShowResult result)
    {
        RestartGame();
    }

    private void RestartGame()
    {
        currentScore = 0;
        ShowMainMenu();
        Debug.Log("Game Restarting...");
    }

    public void EndGame()
    {
        // Implement end game logic (e.g., show game over screen or main menu)
        Time.timeScale = 0;
        ShowMainMenu();
    }
    public void QuitGame()
    {
        Debug.Log("Exiting game.");
        Application.Quit();
    }

    private void UpdateScoreUI()
    {
        if (textScore != null)
        {
            textScore.text = "Score: " + currentScore;
        }
    }

    private void UpdateBestScoreUI()
    {
        if (textBest != null)
        {
            textBest.text = "Best Score: " + bestScore;
        }
    }

    private void UpdateTimeUI()
    {
        if (timeText != null && currentMode == GameMode.TimeAttack)
        {
            timeText.text = "Time: " + timeRemaining;
        }
    }

    

    void Update()
    {
        // Update logic here if needed
    }
}

