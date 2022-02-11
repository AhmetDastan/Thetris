using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(clickButton);
    }


    public void clickButton()
    {
        if(gameObject.name == "Menu")
        {
            SceneManageSystem.LoadNewScene("Menu Scene");
        }
        else if (gameObject.name == "Restart")
        {
            SceneManageSystem.LoadNewScene("Game Scene");
        }
        else
        {
            SceneManageSystem.LoadNewScene("Game Scene");
        }
    }
}
