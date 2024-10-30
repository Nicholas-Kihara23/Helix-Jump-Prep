
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerX : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textBest;
    [SerializeField] private TextMeshProUGUI timeText;

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();

    }

    // Update is called once per frame

    private void Update()
    {
        
    }

   public void UpdateScore()
    {
        Debug.Log("Updating scores: Best = " + GameManagerX.singleton.bestScore + ", Current = " + GameManagerX.singleton.currentScore);
        
        if (GameManagerX.singleton == null)
        {
            Debug.LogError("GameManagerX.singleton is null!");
            return; // Exit if GameManagerX is not initialized
        }

        if (textBest == null)
        {
            Debug.LogError("textBest is null!");
            return; // Early exit if textBest is not assigned
        }

        if (textScore == null)
        {
            Debug.LogError("textScore is null!");
            return; // Early exit if textScore is not assigned
        }

        // Log before updating the text to see values
        Debug.Log("Updating scores: Best = " + GameManagerX.singleton.bestScore + ", Current = " + GameManagerX.singleton.currentScore);

        textBest.text = "Best: " + GameManagerX.singleton.bestScore.ToString("N0");
        textScore.text = "Score: " + GameManagerX.singleton.currentScore.ToString("N0");



 


    }
    public void UpdateScore(int newScore)
    {
        textScore.text = "Score: " + newScore;
    }

    public void UpdateTimeDisplay(int timeRemaining)
    {
        timeText.text = "Time: " + timeRemaining;
    }

    public void TogglePauseScreen(bool show)
    {
        pauseMenu.SetActive(show);
    }
}
