using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class go2 : MonoBehaviour
{
    static public int temp = 1;

    private void Awake()
    {
        Debug.Log(" go 2 awake start");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 999999; j++)
            {

            }
            Debug.Log("ananas " + i);
        }
        Debug.Log("go 2  awake finished");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("go 2 start");
    }
}
