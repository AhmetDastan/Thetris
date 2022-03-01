using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightingButton : MonoBehaviour
{
    [SerializeField] private GameObject go;

    private Sprite gameObjectSprite;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(lightOnOf);
        this.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    }

    void lightOnOf()
    {
        if (go.activeSelf == true)
        {
            go.SetActive(false);
            this.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        else 
        {
            this.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            go.SetActive(true);
        }
    }
}
