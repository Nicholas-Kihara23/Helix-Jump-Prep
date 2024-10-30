using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalX : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManagerX.singleton.NextLevel();
        Debug.Log("Next Level Platform has been reached");
        
    }

}
