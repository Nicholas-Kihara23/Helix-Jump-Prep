using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour // Ensure the class inherits from MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (GameManagerX.singleton != null)
        {
            GameManagerX.singleton.AddScore(1);

            FindObjectOfType<BallControllerX>().perfectPass++; // Ensure perfectPass is public or accessible
          
            Debug.Log("perfectPass is increased to: " + FindObjectOfType<BallControllerX>().perfectPass);
        }
        else
        {
            Debug.LogError("GameManagerX.singleton is null. Ensure GameManagerX is present in the scene.");
        }
    }
}
