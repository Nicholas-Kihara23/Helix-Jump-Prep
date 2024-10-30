using UnityEngine;
using UnityEngine.Advertisements;

public class AdListener : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    // Example method to load an ad
    public void LoadAd(string RestartAd)
    {
        Advertisement.Load(RestartAd, this);
    }

    // IUnityAdsLoadListener methods
    public void OnUnityAdsAdLoaded(string RestartAd)
    {
        Debug.Log("Ad successfully loaded: " + RestartAd);
        Advertisement.Show(RestartAd, this); // Automatically show the ad after loading
    }

    public void OnUnityAdsFailedToLoad(string RestartAd, UnityAdsLoadError error, string message)
    {
        //Debug.LogError($"Failed to load ad: RestartAd}. Error: {error} - {message}");
    }

    // IUnityAdsShowListener methods
    public void OnUnityAdsShowComplete(string aRestartAd, UnityAdsShowCompletionState showCompletionState)
    {
        //Debug.Log("Ad completed for " + RestartAd + " with state: " + showCompletionState);
    }

    public void OnUnityAdsShowFailure(string RestartAd, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad show failed for {RestartAd}. Error: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string RestartAd)
    {
       // Debug.Log("Ad started for " +   );
    }

    public void OnUnityAdsShowClick(string RestartAd)
    {
        Debug.Log("Ad clicked for " + RestartAd);
    }
}



