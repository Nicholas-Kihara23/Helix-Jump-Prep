using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;

public class LeaderboardInitializer : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
}
