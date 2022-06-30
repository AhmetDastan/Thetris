using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    [SerializeField] public GridManager gridManager;
    public AudioManager audioManager;

    [SerializeField] float LockDelayFrameAmount;
    [SerializeField] float LockDelayFrameCounter;

    [SerializeField] float LockDelayMaxFrameAmount;
    [SerializeField] float LockDelayMaxFrameCounter;

    int currentFrame = 0;
    int tempCurrentFrame = 0;

    internal bool isHardDrop = false;
    internal bool isSoftDrop = false;

    private float frameCounter = 0;
    GameObject tempGo;
    private void Start()
    {
        if (gameObject != null)
        {
            LockDelayFrameAmount = 30f;
            LockDelayFrameCounter = 0;

            LockDelayMaxFrameAmount = 120;
            LockDelayMaxFrameCounter = 0;

            currentFrame = FindObjectOfType<GameHandle>().currentFrame;
            tempCurrentFrame = currentFrame;

            audioManager = FindObjectOfType<AudioManager>();

            isHardDrop = false;
            isSoftDrop = false;
        }
    }

    void Update()
    {
        if(gameObject != null)
        {
            BlockMovement();

            if (isHardDrop)
            {
                currentFrame = currentFrame / 20;
            }
            else if (isSoftDrop)
            {
                currentFrame = 5;
            }
            else currentFrame = tempCurrentFrame;

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
        if (InputManager.isLeftSliding && !isHardDrop && !isSoftDrop)
        {
            InputManager.isLeftSliding = false;
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        if (InputManager.isRightSliding && !isHardDrop && !isSoftDrop)
        {
            InputManager.isRightSliding = false;
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        if (InputManager.isRightRotation && !isHardDrop && !isSoftDrop && (FindObjectOfType<GameHandle>().currentBlock.tag != "O Block"))
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

        if (InputManager.isLeftRotation && !isHardDrop && !isSoftDrop && (FindObjectOfType<GameHandle>().currentBlock.tag != "O Block"))
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
            isHardDrop = true;
            LockDelayFrameCounter = LockDelayFrameAmount;
            LockDelayMaxFrameCounter = LockDelayMaxFrameAmount;
        }

        isSoftDrop = InputManager.isSoftDrop ? isSoftDrop = true : isSoftDrop = false;


        GosthPiece(gameObject);
    }

    private void FallDown()
    {
        transform.position -= new Vector3(0, 1, 0);
        if (!ValidMove())
        {
            transform.position += new Vector3(0, 1, 0);
        }
        if (isSoftDrop)
        {
            FindObjectOfType<GameHandle>().ScoreUpdate(1);
        }
        if (isHardDrop)
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
            FindObjectOfType<GameHandle>().isBlockLocked = true;
            FindObjectOfType<GameHandle>().gameUiManager.holdBlock.replaceable = true;
            gridManager.ClearLine();
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