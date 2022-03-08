using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{ 
    [SerializeField] internal BlockSpawner blockSpawner;
    [SerializeField] internal GameUiManager gameUiManager;

    [SerializeField] internal int levelNum = 0;
    [SerializeField] internal int totalLine = 0;
    [SerializeField] internal int haveLineNextLvl = 0;
    [SerializeField] internal int countLineNextLvl = 0;

    internal int currentFrame = 48;
    internal int totalScore = 0;

    internal bool isBlockLocked = false;
    internal bool isNeedNewBlock = false;
    internal bool isStartNewGame = false;
    internal bool isBlocksBreak = false;

    [SerializeField] internal GameObject currentBlock;
    [SerializeField] internal GameObject gameOverPanel;

    public SaveObject saveObject;
    public AudioManager audioManager; 

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        isBlockLocked = false;
        isNeedNewBlock = false;
        isBlocksBreak = false; 
        GameStage.isStartedNewGame = true;

        AdjustNewGameProporties(); 

        LoadObjectFromSaveFile(); 
        //SaveManager.DeleteFile(); 
    }

    void Update()
    {
        
        if ( GameStage.isStartedNewGame && currentBlock == null) //GameStage.isGameScene &&
        {
            gameOverPanel.SetActive(false);
            isStartNewGame = false;
            currentFrame = LevelConstant.getFrameAmount(levelNum);
            currentBlock = blockSpawner.SpawnBlock();
        }

        if (isNeedNewBlock || isBlockLocked)
        {
            GameStage.IsGameOver(currentBlock);
            if (GameStage.isGameOver && isBlockLocked)
            {
                Debug.Log("Game Over ! ");
                gameOverPanel.SetActive(true);
                audioManager.AdjustVolume("MainMusic", 0);
                audioManager.Play("GameOver");
                audioManager.AdjustVolume("GameOver", 0.6f);

                SaveObjectToSaveFileForGameOver();
            }
            else
            {
                currentFrame = LevelConstant.getFrameAmount(levelNum);
                currentBlock = blockSpawner.SpawnBlock();
                
            }
            isNeedNewBlock = false;
            isBlockLocked = false;
            GameStage.isStartedNewGame = false;

        }

        if (isBlocksBreak)
        {
            isBlocksBreak = false;
            if(countLineNextLvl > haveLineNextLvl)
            {
                countLineNextLvl -= LevelConstant.TargetLineAmountInLevel(levelNum);
                LevelUp();
            }
        }
    }
    void LevelUp()
    {
        levelNum++;
        haveLineNextLvl = LevelConstant.TargetLineAmountInLevel(levelNum);
    }

    void AdjustNewGameProporties()
    {
        levelNum = 0;
        totalLine = 0;
        haveLineNextLvl = LevelConstant.TargetLineAmountInLevel(levelNum);
    }

    public void BlocksBreak(int lineAmount)
    {
        totalLine += lineAmount;
        countLineNextLvl += lineAmount;
        ScoreUpdate(LevelConstant.getScoreAmount(levelNum, lineAmount));
        isBlocksBreak = true;
    }
    public void ScoreUpdate(int scoreAmount)
    {
        totalScore += scoreAmount;
    }
   
    void LoadObjectFromSaveFile()
    {
        if(saveObject == null)
        {
            Debug.Log("save file cold not find");
        }
        else
        {
            saveObject = SaveManager.Load();
        }
    }
    void SaveObjectToSaveFileForGameOver()
    {
       RearrangeBestScore(totalScore);

        SaveManager.Save(saveObject);
    }

    void RearrangeBestScore(int lastScore)
    { 
        if(saveObject == null)
        {
            return;
        }
        int tempInt = 0;
        for (int i = 0; i < saveObject.highScores.Length; i++)
        {
            if(lastScore > saveObject.highScores[i])
            {
                for (int j = i; j < (saveObject.highScores.Length-1); j++)
                {
                    if (saveObject.highScores[j] == 0)
                    {
                        saveObject.highScores[j] = lastScore;
                        break;
                    }
                    tempInt = saveObject.highScores[j];
                    saveObject.highScores[j] = lastScore;
                    lastScore = tempInt;

                }
                break;
            }
            else if(saveObject.highScores[i] == 0)
            {
                saveObject.highScores[i] = lastScore;
                break;
            }
            else
            {
                continue;
            }
        }
    }
} 