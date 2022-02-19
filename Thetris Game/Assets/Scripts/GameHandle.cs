using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
    private static GameHandle _instance = null;


    [SerializeField] internal BlockSpawner blockSpawner;
    [SerializeField] internal GameUiManager gameUiManager;

    public int levelNum = 0;
    internal int currentFrame = 48;
    internal float currentScore = 0;

    internal bool isBlockLocked = false;
    internal bool isNeedNewBlock = false;
    internal bool isStartNewGame = false;

    [SerializeField] internal GameObject currentBlock;
    [SerializeField] internal GameObject gameOverPanel;

    public SaveObject saveObject;
    public AudioManager audioManager;
   /* private void Awake()
    {
        Debug.Log("bananas");

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        isStartNewGame = true;

    }*/

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        isBlockLocked = false;
        isNeedNewBlock = false;
        //isStartNewGame = true;
        GameStage.isStartedNewGame = true;
        levelNum = 0;
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
                //save score birde restart durumu olmali gameover ayru bi tantana
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
/*
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
    */
}
/* 
    notlar  =>

1) ui manager da game over olunca main music sesi 0da ayarlaniyo, play veya menu tekrarin da basinlan menu ile tekrardan main musihi cal
 
 */