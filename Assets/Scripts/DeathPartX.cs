using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPartX : MonoBehaviour
{
    //to change the color of a piece
    private void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.red;
        
    }
    public void HitDeathPart()
    {
        GameManagerX.singleton.RestartLevel();
        
    
    }

}
