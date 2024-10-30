using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardItemPrefab; // Prefab for each leaderboard entry
    public Transform content; // The Content GameObject in the Scroll View
    
    private List<int> scores = new List<int>(); // List to store the scores

    // Method to add a score to the leaderboard
    public void AddScore(int newScore)
    {
        scores.Add(newScore);
        scores.Sort((a, b) => b.CompareTo(a)); // Sort scores in descending order

        // Optionally limit the leaderboard to the top 10 scores
        if (scores.Count > 10)
        {
            scores.RemoveAt(scores.Count - 1);
        }

        UpdateLeaderboardDisplay();
    }

    // Method to update the leaderboard display
    private void UpdateLeaderboardDisplay()
    {
        // Clear existing entries
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        
       
        foreach (var score in scores)
        {
            GameObject item = Instantiate(leaderboardItemPrefab, content);

            item.GetComponent<TextMeshProUGUI>().text = score.ToString();
        }
    }
}
