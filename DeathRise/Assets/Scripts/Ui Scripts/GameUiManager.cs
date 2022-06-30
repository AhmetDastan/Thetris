using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{

    [SerializeField] internal GameHandle gameHandle;
    [SerializeField] internal HoldBlock holdBlock;
    [SerializeField] internal UiBlockQueue uiBlockQueue;
    GameObject gameOverPanel;

    [SerializeField] internal GameObject pauseButton;

    [SerializeField] internal Text line;
    [SerializeField] internal Text level;
    [SerializeField] internal Text score;

    [SerializeField] internal Sprite[] blocksSprite; 


    private void Awake()
    { 
        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        UpgradeGameUiData();  
        if (GameStage.isGameOver)
        { 
            OpenGameOverPanel();
        }
    }
    void UpgradeGameUiData()
    {
        line.text = "Line " + gameHandle.totalLine.ToString();
        level.text = "Lvl " + gameHandle.levelNum.ToString();
        if(gameHandle.saveObject != null && gameHandle.saveObject.highScores[0] > 0)
        {
            score.text = "Score " + gameHandle.totalScore.ToString() + " / " + gameHandle.saveObject.highScores[0];
        }
        else score.text = "Score " + gameHandle.totalScore.ToString();
    }
     
    public void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);  
    }

    public void CloseGameOverPanel()
    { 
        gameOverPanel.SetActive(false);  
    }
}
