using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{

    public double[] highScores = new double[10];

    public int currentLevel = 0;
    public int currentScore = 0;
    public static Dictionary<Vector2, Tile> mapSave;
}
