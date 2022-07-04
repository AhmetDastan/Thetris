using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightingButton : MonoBehaviour
{
    private bool isLambOpen = true;
     
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(lightOnOf);
        this.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); 
    }

    void lightOnOf()
    {

        if (isLambOpen == true)
        {
            isLambOpen = false; 
            this.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        else
        {
            isLambOpen = true; 
            this.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); 
        }
    }
}
