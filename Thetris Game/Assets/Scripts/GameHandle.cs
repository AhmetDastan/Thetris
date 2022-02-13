﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
    private static GameHandle _instance = null;


    [SerializeField] internal BlockSpawner blockSpawner;
    [SerializeField] internal HoldBlock holdBlock;

    public int levelNum = 0;
    internal int currentFrame = 48;
    internal float currentScore = 0;

    internal bool isBlockLocked = false;
    internal bool isNeedNewBlock = false;
    internal bool isStartNewGame = false;

    internal GameObject currentBlock;
    [SerializeField] internal GameObject gameOverPanel;

    public SaveObject saveObject;
    public AudioManager audioManager;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isBlockLocked = false;
        isNeedNewBlock = false;
        isStartNewGame = true;
        levelNum = 0;
        audioManager.Play("Main Music");
    }

    void Update()
    {
        if (isStartNewGame)
        {
            gameOverPanel.SetActive(false);
            isStartNewGame = false;
            currentFrame = LevelConstant.getFrameAmount(levelNum);
            currentBlock = blockSpawner.SpawnBlock();
        }
        if (isNeedNewBlock || isBlockLocked)
        {
            if (IsGameOver() && isBlockLocked)
            {
                Debug.Log("Game Over ! ");
                gameOverPanel.SetActive(true);
            }
            else
            {
                currentFrame = LevelConstant.getFrameAmount(levelNum);
                currentBlock = blockSpawner.SpawnBlock();
            }
            isNeedNewBlock = false;
            isBlockLocked = false;
            
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            saveObject = SaveManager.Load();

        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Save girecem");
            SaveManager.Save(saveObject);
        }
    }

    bool IsGameOver()
    {
        int roundedX, roundedY;
        foreach (Transform child in currentBlock.transform)
        {
            roundedY = Mathf.RoundToInt(child.transform.position.y);
            roundedX = Mathf.RoundToInt(child.transform.position.x);

            if (roundedY > 18 && (roundedX >= 4 && roundedX <= 6))
            {
                return true;
            }
        }
        return false;
    }

}
