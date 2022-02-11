using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    [SerializeField] public GridManager gridManager;

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
        LockDelayFrameAmount = 30f;
        LockDelayFrameCounter = 0;

        LockDelayMaxFrameAmount = 120;
        LockDelayMaxFrameCounter = 0;

        currentFrame = FindObjectOfType<GameHandle>().currentFrame;
        tempCurrentFrame = currentFrame;
    }

    void Update()
    {
        
        BlockMovement();

        if (isHardDrop)
        {
            currentFrame = currentFrame / 20;
        }
        else if (isSoftDrop)
        {
            currentFrame = (tempCurrentFrame / 3  );
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

    void BlockMovement()
    {
        if (InputManager.isPressedLeft)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove(gameObject))
                transform.position -= new Vector3(-1, 0, 0);
            LockDelayFrameCounter = 0;
        }
        if (InputManager.isPressedRight)
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove(gameObject))
                transform.position -= new Vector3(1, 0, 0);
            LockDelayFrameCounter = 0;
        }
        if (InputManager.isPressedM && (FindObjectOfType<GameHandle>().currentBlock.tag != "O Block"))
        {
            transform.RotateAround(transform.position, Vector3.forward, 90);

            if (!ValidMove(gameObject))
            {
                if (!WallKick())
                {
                    transform.RotateAround(transform.position, Vector3.back, 90);
                }
            }
            LockDelayFrameCounter = 0;
        }

        if (InputManager.isPressedN && (FindObjectOfType<GameHandle>().currentBlock.tag != "O Block"))
        {
            transform.RotateAround(transform.position, Vector3.back, 90);

            if (!ValidMove(gameObject))
            {
                if (!WallKick())
                {
                    transform.RotateAround(transform.position, Vector3.forward, 90);
                }
            }
            LockDelayFrameCounter = 0;
        }
        if (InputManager.isPressedSpace) isHardDrop = true;

        isSoftDrop = InputManager.isPressedDown ? isSoftDrop = true : isSoftDrop = false;


        GosthPiece(gameObject);
    }

    private void FallDown()
    {
        transform.position -= new Vector3(0, 1, 0);
        if (!ValidMove(gameObject))
        {
            transform.position += new Vector3(0, 1, 0);
        }
    }

    private bool WallKick()
    {
        if (FindObjectOfType<GameHandle>().currentBlock.tag == "I Block")
        {
            transform.position += new Vector3(-2, 0, 0);
            if (!ValidMove(gameObject))
            {
                transform.position += new Vector3(4, 0, 0);
                if (!ValidMove(gameObject))
                {
                    transform.position += new Vector3(-2, 2, 0);
                    if (!ValidMove(gameObject))
                    {
                        transform.position += new Vector3(0, -2, 0);
                        return false;
                    }
                }
            }
            LockDelayFrameCounter = 0;
            return true;
        }
        else if (FindObjectOfType<GameHandle>().currentBlock.tag != "I Block")
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove(gameObject))
            {
                transform.position += new Vector3(2, 0, 0);
                if (!ValidMove(gameObject))
                {
                    transform.position += new Vector3(-1, 1, 0);
                    if (!ValidMove(gameObject))
                    {
                        transform.position += new Vector3(0, -1, 0);
                        return false;
                    }
                }
            }
            LockDelayFrameCounter = 0;
            return true;
        }
        return true;
    }

    private void BlockStop()
    {
        if (isGrounded())
        {
            this.enabled = false;
            gridManager.FillTheGridByBlock(gameObject);
            FindObjectOfType<GameHandle>().isBlockLocked = true;
            FindObjectOfType<GameHandle>().holdBlock.replaceable = true;
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

    public bool ValidMove(GameObject go)
    {
        int roundedX, roundedY;
        Tile tile;
        foreach(Transform child in go.transform)
        {
            roundedX = Mathf.RoundToInt(child.transform.position.x);
            roundedY = Mathf.RoundToInt(child.transform.position.y);

            tile = GridManager.GetTileAtPosition(new Vector2(roundedX, roundedY));

            if ((tile == null || !tile._isEmpty) ) 
            {
                return false;
            }
        }
        return true;
    }

    public void GosthPiece(GameObject gameObject)
    {
        int roundedX, roundedY;
        tempGo = Instantiate(gameObject);  // i know that is not master piece
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
}