
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStage : MonoBehaviour
{
    public static bool isGameScene = false;
    public static bool isMenuScene = false;
    public static bool isStartedNewGame = true;
    public static bool isGameOver = false;


    public static GameStage Instance;

    private void Awake()
    {
        DefineScene(SceneManageSystem.CurrentScene());
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void DefineScene(string sceneName)
    {
        isGameScene = sceneName == "Game Scene" ? isGameScene = true : isGameScene = false;
        isMenuScene = sceneName == "Menu Scene" ? isMenuScene = true : isMenuScene = false;
        if (sceneName == "Game Scene")
        {
            isGameScene = true;
            isStartedNewGame = true;
        }
        isGameOver = false;
    }


    public static void IsGameOver(GameObject currentBlock)
    {
        if (currentBlock != null)
        {
            int roundedX, roundedY;
            foreach (Transform child in currentBlock.transform)
            {
                roundedY = Mathf.RoundToInt(child.transform.position.y);
                roundedX = Mathf.RoundToInt(child.transform.position.x);

                if (roundedY > 18 && (roundedX >= 4 && roundedX <= 6))
                {
                    isGameOver = true;
                    break;
                }
                else
                {
                    isGameOver = false;
                }
            }
        } 
    }
}