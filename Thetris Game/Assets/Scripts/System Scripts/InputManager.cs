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
    private bool isBeginTouch = false;
    private bool isMovedTouch = false;
    private bool isStationaryTouch = false;

    private float oneUnitScreenWidth = 0f;
    private float oneUnitScreenHeight = 0f;

    private float currentTouchDeltaPositionX = 0;
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
                //isBeginTouch = true;
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
                //                Debug.Log("current delta + ended " + currentTouchDeltaPositionY + " ve " + oneUnitScreenHeight + " ve " + isEndedPhase);
            }
            else isEndedPhase = false;

            if (touch.phase == TouchPhase.Stationary)
            {
                isStationaryTouch = true;

            }
            else isStationaryTouch = false;

            currentTouchDeltaPositionY = touch.deltaPosition.y;

        }

        Debug.Log("isstationar " + isStationaryTouch);

        //Debug.Log("1 " + (touch.deltaPosition.y < -oneUnitScreenHeight && isMovedTouch));
        //Debug.Log("2 " + (isSoftDrop && isStationaryTouch));

        //Debug.Log("currentDelta" + currentTouchDeltaPositionY + " ve " + touch.deltaPosition.y + " ve " + oneUnitScreenHeight);
        //soft drope
        if ( (touch.deltaPosition.y < -oneUnitScreenHeight && isMovedTouch ) || (isSoftDrop && (isStationaryTouch || isMovedTouch) ))
        {
            isSoftDrop = true;
            Debug.Log("is soft " + isSoftDrop);
        }
        else isSoftDrop = false;



        //Debug.Log("current delta + ended " + touch.deltaPosition.y + " " +currentTouchDeltaPositionY + " ve " + oneUnitScreenHeight + " ve " + isEndedPhase);
        /*  if(currentTouchDeltaPositionY == 0)
          {
              currentTouchDeltaPositionY = touch.deltaPosition.y;
          }*/

        //harddrope
        /*if (currentTouchDeltaPositionY < -oneUnitScreenHeight && isEndedPhase)
        {
            isHardDrop = true;
        }
        else
        {
            isHardDrop = false;
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

        //hold area
        if (currentTouchDeltaPositionY > oneUnitScreenHeight && isEndedPhase)
        {
            isBlockHolded = true;
        }
        else
        {
            isBlockHolded = false;
        }

        
        

        //Rotation
        if ((Mathf.Abs(firstTorucPos.x - endTouchPos.x) < clickEventAmount) && isEndedPhase)
        {
            if (touchPos.x < (Screen.width / 2))
            {
                isRightRotation = true;
            }
            else
            {
                isLeftRotation = true;
            }
        } */ 
    }
}
