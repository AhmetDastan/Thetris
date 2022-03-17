using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    private Touch touch;
    private Vector3 touchPos;
    private Vector3 currentTouchPos;
    private Vector3 firstTorucPos;
    private Vector3 endTouchPos;

    internal bool isAvailableTouch = true;
    private bool isEndedPhase = false; 
    private bool isMovedTouch = false;
    private bool isStationaryTouch = false;

    private float oneUnitScreenWidth = 0f;
    private float oneUnitScreenHeight = 0f;
     
    private float currentTouchDeltaPositionY = 0;

    private float clickEventAmount = 5f;

    public static bool isLeftSliding = false;
    public static bool isRightSliding = false;
    public static bool isLeftRotation = false;
    public static bool isRightRotation= false;
    public static bool isSoftDrop = false;
    public static bool isHardDrop = false;
    public static bool isBlockHolded = false;


    public static InputManager Instance;

    private void Awake()
    {
        var verticalSize = Camera.main.orthographicSize * 2.0;
        var horizontalSize = verticalSize * Screen.width / Screen.height;
        
        oneUnitScreenWidth = Screen.width / (float)horizontalSize;
        oneUnitScreenHeight = Screen.height / (float)verticalSize;
        clickEventAmount = 5;

        isEndedPhase = false;
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

    // Update is called once per frame
    void Update()
    { 
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            currentTouchPos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                touchPos = touch.position;
                firstTorucPos = touch.position; 
            }

            if (touch.phase == TouchPhase.Moved)
            {
                isMovedTouch = true;

            }
            else isMovedTouch = false;


            if (touch.phase == TouchPhase.Ended)
            {
                isEndedPhase = true;
                endTouchPos = touch.position;
            }
            else
            {
                currentTouchDeltaPositionY = touch.deltaPosition.y;
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                isStationaryTouch = true;

            }
            else isStationaryTouch = false;
        }

        //sliding
        if ((Mathf.Abs(currentTouchPos.x - touchPos.x) > oneUnitScreenWidth))
        {
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
        if ((Mathf.Abs(firstTorucPos.x - endTouchPos.x) < clickEventAmount) && isEndedPhase)
        {
            isEndedPhase = false;
            if (touchPos.x < (Screen.width / 2))
            {
                isRightRotation = true;
            }
            else
            {
                isLeftRotation = true;
            }
        }


        //harddrope
        if (currentTouchDeltaPositionY < -4*oneUnitScreenHeight )
        {
            isHardDrop = true;
        }
        else
        {
            isHardDrop = false;
        }
        //soft drop 
        if ( touch.deltaPosition.y < -oneUnitScreenHeight || ( (isMovedTouch || isStationaryTouch) && isSoftDrop) )
        {
            isSoftDrop = true;

        }
        else isSoftDrop = false;


        //hold area
        if (currentTouchDeltaPositionY > 2 * oneUnitScreenHeight )
        {
            isEndedPhase = false;
            isBlockHolded = true;
        }
        else
        {
            isBlockHolded = false;
        }

        isEndedPhase = false;
        currentTouchDeltaPositionY = 0;
    }
}
