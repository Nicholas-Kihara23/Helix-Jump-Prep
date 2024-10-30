using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    public void TogglePause()
    {
        bool isPaused = Time.timeScale == 0;
        Time.timeScale = isPaused ? 1 : 0;
        pausePanel.SetActive(!isPaused);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1; // Resume time before exiting
        Debug.Log("Returning to Main Menu");
        // Logic to load the Main Menu scene
    }
}

