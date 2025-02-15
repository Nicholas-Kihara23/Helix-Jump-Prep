using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Leveel
{
    [Range(1, 11)]
    public int partCount = 11;
    [Range(0, 11)]
    public int deathPartCount = 1;
}

[CreateAssetMenu(fileName = "New Stage", menuName = "Stages/Stage")]
public class StageX : ScriptableObject
{
   
    
    public Color stageBackgroundColor = Color.gray;
    public Color stageLevelPartColor = Color.white;
    public Color stageBallColor = Color.red;

    public List<Leveel> levels = new List<Leveel>();




    
}
