using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{

    [SerializeField] internal GameHandle gameHandle;
    [SerializeField] internal HoldBlock holdBlock;
    [SerializeField] internal UiBlockQueue uiBlockQueue;

    [SerializeField] internal Text line;
    [SerializeField] internal Text level;
    [SerializeField] internal Text score;

    [SerializeField] internal Sprite[] blocksSprite;


    void Update()
    {
        UpgradeGameUiData();
    }
    void UpgradeGameUiData()
    {
        line.text = "Line " + gameHandle.totalLine.ToString();
        level.text = "Lvl " + gameHandle.levelNum.ToString();
        score.text = "Score " + gameHandle.totalScore.ToString();
    }
}
