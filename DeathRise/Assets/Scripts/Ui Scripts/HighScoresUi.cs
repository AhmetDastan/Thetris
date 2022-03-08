using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresUi : MonoBehaviour
{
    private float textColliderHigh = 0;
    private float highScoureTextAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        textColliderHigh = 100;
        highScoureTextAmount = 10;

        GameObject newGO = new GameObject("myTextGO");
        newGO.transform.SetParent(this.transform);

        Text myText = newGO.AddComponent<Text>();
        myText.text = "Ta-dah!";

        GameObject a = new GameObject("myTextGO");
        a.transform.SetParent(this.transform);

        Text s = a.AddComponent<Text>();
        s.text = "Ta-dah!";
    } 

    void GenerateHighScoreTexts()
    {
        for (int i = 0; i < highScoureTextAmount; i++)
        {
            GameObject newGO = new GameObject("myTextGO");
            newGO.transform.SetParent(this.transform);

            Text myText = newGO.AddComponent<Text>();
            myText.text = "Ta-dah!";
        }
    }

}
