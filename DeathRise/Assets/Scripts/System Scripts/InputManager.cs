using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    private Touch touch;
    private Vector3 touchPos;
    private Vector3 currentTouchPos; 

    internal bool isAvailableTouch = true; 
    private bool isMovedTouch = false;
    private bool isStationaryTouch = false;
    private bool isEndedTouch = false;

    private float oneUnitScreenWidth = 0f;
    private float oneUnitScreenHeight = 0f;

    private float currentTouchDeltaPositionY = 0f;


    public static bool isLeftSliding = false;
    public static bool isRightSliding = false;
    public static bool isLeftRotation = false;
    public static bool isRightRotation = false;
    public static bool isSoftDrop = false;
    public static bool isHardDrop = false;
    public static bool isBlockHolded = false;
    public static bool isRotationAvailable = false;
    public static bool isSlidingAvailable = false;

    public static InputManager Instance;

    private void Awake()
    {
        
         
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

    private void Start()
    {
        var verticalSize = Camera.main.orthographicSize * 2.0;
        var horizontalSize = verticalSize * Screen.width / Screen.height;

        oneUnitScreenWidth = Screen.width / (float)horizontalSize;
        oneUnitScreenHeight = Screen.height / (float)verticalSize;
    }
     
    void Update()
    { 
        if (!PauseButton.isGamePaused)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                currentTouchPos = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    isRotationAvailable = true; 
                    touchPos = touch.position; 
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    isRotationAvailable = false;
                    isMovedTouch = true;
                    if(!isSoftDrop)
                        isSlidingAvailable = true;
                }
                else isMovedTouch = false;


                if (touch.phase == TouchPhase.Ended)
                { 
                    isEndedTouch = true;
                }
                else
                {
                    currentTouchDeltaPositionY = touch.deltaPosition.y; 

                    if (currentTouchDeltaPositionY > 999999999) // if i just click, defined value will bigger value 
                        currentTouchDeltaPositionY = 0f; 

                    isEndedTouch = false;
                }

                if (touch.phase == TouchPhase.Stationary)
                {
                    isStationaryTouch = true;

                }
                else isStationaryTouch = false;

            }

            //sliding
            if ((Mathf.Abs(currentTouchPos.x - touchPos.x) > oneUnitScreenWidth) && isSlidingAvailable)
            {
                isRotationAvailable = false;
                isSlidingAvailable = false;
                if (touchPos.x < currentTouchPos.x)
                {
                    isRightSliding = true;
                }
                else
                {
                    isLeftSliding = true;
                }
                touchPos = currentTouchPos;
            }

            //Rotation
            if (isRotationAvailable && !isMovedTouch && isEndedTouch) 
            {
                isRotationAvailable = false;
                if (touchPos.x < (Screen.width / 2))
                {
                    isRightRotation = true;
                }
                else
                {
                    isLeftRotation = true;
                }
            }

            //soft drop 
            if ( (currentTouchDeltaPositionY < -oneUnitScreenHeight) || ((isMovedTouch || isStationaryTouch) && isSoftDrop))
            {
                isSoftDrop = true;
                isSlidingAvailable = false;
                isRotationAvailable = false;
            }
            else isSoftDrop = false;

            
            //harddrope
            if ((currentTouchDeltaPositionY < -3 * oneUnitScreenHeight))
            {
                isSlidingAvailable = false;
                isRotationAvailable = false;
                isHardDrop = true;
            }
            else
            {
                isHardDrop = false;
            }


            //hold area
            if (currentTouchDeltaPositionY > (5 * oneUnitScreenHeight))
            { 
                isBlockHolded = true;
            }else isBlockHolded = false;

            currentTouchDeltaPositionY = 0;
        } 
    }
}
