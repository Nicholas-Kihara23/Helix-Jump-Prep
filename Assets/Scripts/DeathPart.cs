﻿using UnityEngine;

public class DeathPart : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void HittedDeathPart()
    {
        GameManager.singleton.RestartLevel();
    }
}
