using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonScript : MonoBehaviour
{

    void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(clickButton); 
    } 
    public void clickButton()
    {
        if (gameObject.name == "Menu")
        {
            SceneManageSystem.LoadNewScene("Menu Scene");
            FindObjectOfType<AudioManager>().Play("MainMusic");
            FindObjectOfType<AudioManager>().AdjustVolume("MainMusic", 0.5f);
            FindObjectOfType<AudioManager>().AdjustVolume("GameOver", 0);
        }
        else if (gameObject.name == "Restart")
        {
            SceneManageSystem.LoadNewScene("Game Scene");
            FindObjectOfType<AudioManager>().Play("MainMusic");
            FindObjectOfType<AudioManager>().AdjustVolume("MainMusic", 0.5f);
            FindObjectOfType<AudioManager>().AdjustVolume("GameOver", 0);
        }
        else if (gameObject.name == "Play")
        {
            SceneManageSystem.LoadNewScene("Game Scene");
        }
    } 
}
