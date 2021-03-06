using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    [SerializeField] public GridManager gridManager;
    public AudioManager audioManager;
    internal GameHandle gameHandle;

    [SerializeField] float LockDelayFrameAmount;
    [SerializeField] float LockDelayFrameCounter;

    [SerializeField] float LockDelayMaxFrameAmount;
    [SerializeField] float LockDelayMaxFrameCounter;

    int currentFrame = 0;
    int hardFropFrame = 0;
    int softDropFrame = 5;
    int tempCurrentFrame = 0;


    private float frameCounter = 0;
    GameObject tempGo;
    private void Start()
    {
        gameHandle = GameObject.FindObjectOfType<GameHandle>();

        LockDelayFrameAmount = 30f;
        LockDelayFrameCounter = 0;

        LockDelayMaxFrameAmount = 120;
        LockDelayMaxFrameCounter = 0;

        currentFrame = gameHandle.currentFrame;
        tempCurrentFrame = currentFrame;
        softDropFrame = 5;

        audioManager = FindObjectOfType<AudioManager>();
         
    }

    void Update()
    {
        if(gameObject != null)
        {
            BlockMovement();
            
            if (InputManager.isHardDrop || (currentFrame == hardFropFrame))
            {
                hardFropFrame = (currentFrame / 20);
                currentFrame = hardFropFrame;
            }else if (InputManager.isSoftDrop)  
            {
                currentFrame = softDropFrame;
            }else currentFrame = tempCurrentFrame;

            frameCounter += 1;

            if (frameCounter >= currentFrame)
            {

                frameCounter = 0;
                FallDown();
            }

            if (LockDelayFrameCounter >= LockDelayFrameAmount)
            {
                LockDelayFrameCounter = 0;
                BlockStop();
            }
            else
            {
                LockDelayFrameCounter += 1;
            }

            if (isGrounded())
            {
                if (LockDelayMaxFrameCounter >= LockDelayMaxFrameAmount)
                {
                    LockDelayMaxFrameCounter = 0;
                    BlockStop();
                }
                else
                {
                    LockDelayMaxFrameCounter += 1;
                }
            }
        }
    }

    void BlockMovement()
    {
        if (InputManager.isLeftSliding)  
        {
            InputManager.isLeftSliding = false;
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        if (InputManager.isRightSliding  )  
        {
            InputManager.isRightSliding = false;
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        if (InputManager.isRightRotation   && (FindObjectOfType<GameHandle>().currentBlock.tag != "O Block"))  
        {
            InputManager.isRightRotation = false;

            transform.RotateAround(transform.position, Vector3.forward, 90);

            if (!ValidMove())
            {
                if (!WallKick())
                {
                    transform.RotateAround(transform.position, Vector3.back, 90);
                }
            }
        }

        if (InputManager.isLeftRotation  && (FindObjectOfType<GameHandle>().currentBlock.tag != "O Block"))  
        {
            InputManager.isLeftRotation = false;
            transform.RotateAround(transform.position, Vector3.back, 90);

            if (!ValidMove())
            {
                if (!WallKick())
                {
                    transform.RotateAround(transform.position, Vector3.forward, 90);
                }
            }
        }
        if (InputManager.isHardDrop)
        { 
            LockDelayFrameCounter = LockDelayFrameAmount;
            LockDelayMaxFrameCounter = LockDelayMaxFrameAmount;
        }

        GosthPiece(gameObject);
    }

    private void FallDown()
    {
        transform.position -= new Vector3(0, 1, 0);
        if (!ValidMove())
        {
            transform.position += new Vector3(0, 1, 0);
        }
        if (InputManager.isSoftDrop) //isSoftDrop
        {
            FindObjectOfType<GameHandle>().ScoreUpdate(1);
        }
        if (InputManager.isHardDrop) //isHardDrop
        {
            FindObjectOfType<GameHandle>().ScoreUpdate(2);
        }
    }

    private bool WallKick()
    {
        if (FindObjectOfType<GameHandle>().currentBlock.tag == "I Block")
        {
            transform.position += new Vector3(-2, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(4, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(-2, 2, 0);
                    if (!ValidMove())
                    {
                        transform.position += new Vector3(0, -2, 0);
                        return false;
                    }
                }
            }
            return true;
        }
        else if (FindObjectOfType<GameHandle>().currentBlock.tag != "I Block")
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(2, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(-1, 1, 0);
                    if (!ValidMove())
                    {
                        transform.position += new Vector3(0, -1, 0);
                        return false;
                    }
                }
            }
            return true;
        }
        return true;
    }

    private void BlockStop()
    {
        if (isGrounded())
        {
            audioManager.Play("BlockFitting");

            this.enabled = false;
            gridManager.FillTheGridByBlock(gameObject);
            
            gameHandle.isBlockLocked = true;
            gameHandle.gameUiManager.holdBlock.replaceable = true;

            gridManager.ClearLine();
            GameStage.IsGameOver(gameObject);
        }
    }

    private bool isGrounded()
    {
        int roundedX, roundedY;
        Tile tile;
        foreach (Transform child in transform)
        {
            roundedX = Mathf.RoundToInt(child.transform.position.x);
            roundedY = Mathf.RoundToInt(child.transform.position.y);

            roundedY -= 1;

            tile = GridManager.GetTileAtPosition(new Vector2(roundedX, roundedY));

            if (tile == null || !tile._isEmpty)
            {
                return true;
            }
        }
        return false;
    }

    public bool ValidMove()
    {
        if (PauseButton.isGamePaused)
        {
            return false;
        }
        int roundedX, roundedY;
        Tile tile;
        foreach (Transform child in gameObject.transform)
        {
            roundedX = Mathf.RoundToInt(child.transform.position.x);
            roundedY = Mathf.RoundToInt(child.transform.position.y);

            tile = GridManager.GetTileAtPosition(new Vector2(roundedX, roundedY));

            if ((tile == null || !tile._isEmpty))
            {
                return false;
            }
        }
        LockDelayFrameCounter = 0;
        return true;
    }

    public void GosthPiece(GameObject gameObject)
    {
        int roundedX, roundedY;
        tempGo = Instantiate(gameObject);  // i know that is not master piece, it is not efficient
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                GridManager._tiles[new Vector2(i, (j))].ghostBlock.SetActive(false);
            }
        }
        do
        {
            tempGo.transform.position += new Vector3(0, -1, 0);
        } while (ValidMove(tempGo));

        tempGo.transform.position -= new Vector3(0, -1, 0);

        foreach (Transform child in tempGo.transform)
        {
            roundedX = Mathf.RoundToInt(child.transform.position.x);
            roundedY = Mathf.RoundToInt(child.transform.position.y);

            GridManager._tiles[new Vector2(roundedX, roundedY)].ghostBlock.SetActive(true);
        }
        Destroy(tempGo);
    }
    public bool ValidMove(GameObject go)
    {
        int roundedX, roundedY;
        Tile tile;
        foreach (Transform child in go.transform)
        {
            roundedX = Mathf.RoundToInt(child.transform.position.x);
            roundedY = Mathf.RoundToInt(child.transform.position.y);

            tile = GridManager.GetTileAtPosition(new Vector2(roundedX, roundedY));

            if ((tile == null || !tile._isEmpty))
            {
                return false;
            }
        }
        return true;
    }
}