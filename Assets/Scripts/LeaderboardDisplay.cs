using System.Collections;
using System.Collections.Generic;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LeaderboardDisplay : MonoBehaviour
{
    public GameObject scorePrefab;
    public Transform scoreContainer;
    public TextMeshProUGUI loadingText;

    public async void DisplayTopScores()
    {
        loadingText.text = "Loading...";
        try
        {
            var leaderboardId = "your_leaderboard_id";

            // Fetch the leaderboard scores with a limit of 10
            var scoresPage = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, new GetScoresOptions { Limit = 10 });
            loadingText.gameObject.SetActive(false);

            // Access 'scores' property in scoresPage to loop through individual score entries
            if (scoresPage != null && scoresPage.Results != null)  // Adjusted to access 'Results' or 'scores'
            {
                foreach (var score in scoresPage.Results) // Assuming 'Results' is the correct iterable list
                {
                    var scoreInstance = Instantiate(scorePrefab, scoreContainer);
                    scoreInstance.GetComponent<TextMeshProUGUI>().text = $"{score.Rank}. {score.PlayerName}: {score.Score}";
                }
            }
            else
            {
                loadingText.text = "No scores available.";
            }
        }
        catch (Exception e)
        {
            loadingText.text = "Failed to load scores.";
            Debug.LogError("Error loading scores: " + e.Message);
        }
    }
}

