using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdListenerX : MonoBehaviour
{
    public void OnUnityAdsReady(string placementId)
    {
        // Optional: Code to execute when an ad is ready
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Ad Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional: Code to execute when an ad starts
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            // Reward the player
            Debug.Log("Ad Finished - Reward player.");
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Ad was skipped, no reward given.");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log("Ad failed to show.");
        }
    }
}
