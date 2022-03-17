using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{ 
    public static bool isGamePaused = false;

    [SerializeField] private static Text pauseText;
    [SerializeField] Text pausedText;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(PauseButtonClick);


        pausedText.text = "Paused";

        pausedText.text = "";
    }
     

    public static void PauseButtonClick()
    {
        if (!isGamePaused)
        {
            isGamePaused = true; 
        }
        else
        {
            isGamePaused = false; 
        }
    }

    private void Update()
    {
        if (isGamePaused)
        {
            pausedText.text = "Paused";
        }else pausedText.text = "";

    }
}
