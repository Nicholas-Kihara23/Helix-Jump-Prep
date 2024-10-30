using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PassCheckX : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManagerX.singleton != null)
        {
            GameManagerX.singleton.AddScore(1);

            FindObjectOfType<BallControllerX>().perfectPass++;
            

            Debug.Log("perfectPass is increased");

        }
        else
        {
            Debug.LogError("GameManagerX.singleton is null. Ensure GameManagerX is present in the scene.");
        }

    }
}
