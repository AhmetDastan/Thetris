using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        isBlockLocked = false;
        isNeedNewBlock = false;
        isStartNewGame = true;
        levelNum = 0;

    }

    // Update is called once per frame
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
        Debug.Log("saveobject datas " + saveObject.currentLevel + " ve " + saveObject.currentScore);

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
