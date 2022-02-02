using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool isPressedSpace = false;
    public static bool isPressedLeft = false;
    public static bool isPressedRight = false;
    public static bool isPressedDown = false;
    public static bool isPressedM = false;
    public static bool isPressedN = false;


    // Update is called once per frame
    void Update()
    {
        isPressedLeft = (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ? isPressedLeft = true : isPressedLeft = false;

        isPressedRight = (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) ? isPressedRight = true : isPressedRight = false;
        
        isPressedDown = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ? isPressedDown = true : isPressedDown = false;

        isPressedM = (Input.GetKeyDown(KeyCode.M)) ? isPressedM = true : isPressedM = false;

        isPressedN = (Input.GetKeyDown(KeyCode.N)) ? isPressedN = true : isPressedN = false;

        isPressedSpace = (Input.GetKeyDown(KeyCode.Space)) ? isPressedSpace = true : isPressedSpace = false;

    }
}
