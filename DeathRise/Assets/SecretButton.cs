using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SecretButton : MonoBehaviour
{ 
    int counter = 0; 
    // Start is called before the first frame update
    void Start()
    {  
        counter = 0; 
         
        gameObject.GetComponent<Button>().onClick.AddListener(clickButton);

    }
    public void clickButton()
    {
        if (gameObject.name == "SecretButton")
        {
            Debug.Log("flan");
            counter++;
        }
    }
    private void Update()
    {
        if (counter > 3)
        {
            SceneManageSystem.LoadNewScene("Game Scene");
        } 
    } 
}
