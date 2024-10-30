using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public void LoadTimeAttackMode()
    {
        SceneManager.LoadScene("TimeAttackScene");
    }

    public void LoadEndlessMode()
    {
        SceneManager.LoadScene("EndlessScene");
    }

    public void LoadZenMode()
    {
        SceneManager.LoadScene("ZenScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
