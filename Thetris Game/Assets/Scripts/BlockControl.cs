using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    public float defaultBlockSpeed, fastBlockSpeed, currentBlockSpeed;

    [SerializeField] public GridManager gridManager;

    [SerializeField] float LockDelayTime;
    [SerializeField] float LockDelayCounter;

    private void Start()
    {
        LockDelayTime = 1f;
        LockDelayCounter = 0;

        defaultBlockSpeed = 0.9f;
        fastBlockSpeed = 0.1f;
        currentBlockSpeed = defaultBlockSpeed;
        StartCoroutine(FallDown());
    }

    void Update()
    {
        BlockMovement();
        /*if(LockDelayCounter >= LockDelayTime)
        {
            BlockStop();
        }else
        {
            LockDelayCounter += Time.deltaTime;
        }*/
    }

    void BlockMovement()
    {
        gridManager.GosthPiece(gameObject);
        if (InputManager.isPressedLeft)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        if (InputManager.isPressedRight)
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        if (InputManager.isPressedM && (FindObjectOfType<BlockManager>().currentBlock.tag != "O Block"))
        {
            transform.RotateAround(transform.position, Vector3.forward, 90);
           
            if (!ValidMove())
            {
                if (!WallKick())
                {
                    transform.RotateAround(transform.position, Vector3.back, 90);
                }
            }
        }

        if (InputManager.isPressedN && (FindObjectOfType<BlockManager>().currentBlock.tag != "O Block"))
        {
            transform.RotateAround(transform.position, Vector3.back, 90);

            if (!ValidMove())
            {
                if (!WallKick())
                {
                    transform.RotateAround(transform.position, Vector3.forward, 90);
                }
            }
        }
        if (InputManager.isPressedSpace)
        {
            HardDrop();
        }
        currentBlockSpeed = (InputManager.isPressedDown) ? fastBlockSpeed  : defaultBlockSpeed;
    }

    private bool WallKick()
    {
        if (FindObjectOfType<BlockManager>().currentBlock.tag == "I Block")
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
        else if (FindObjectOfType<BlockManager>().currentBlock.tag != "I Block")
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
            LockDelayCounter = 0f;

            this.enabled = false;
            gridManager.FillTheGridByBlock(gameObject);
            FindObjectOfType<BlockManager>().needNewBlock = true;
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

    private IEnumerator FallDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentBlockSpeed);
            LockDelayCounter = 0f;
            transform.position -= new Vector3(0, 1, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);            
                yield break;
            }
        }
    }

    private void HardDrop()
    {
        do{
            transform.position += new Vector3(0, -1, 0);
        } while (ValidMove());

        transform.position -= new Vector3(0, -1, 0);

        BlockStop();
    }

    public bool ValidMove()
    {
        int roundedX, roundedY;
        Tile tile;
        foreach(Transform child in transform)
        {
            roundedX = Mathf.RoundToInt(child.transform.position.x);
            roundedY = Mathf.RoundToInt(child.transform.position.y);

            tile = GridManager.GetTileAtPosition(new Vector2(roundedX, roundedY));

            if (tile == null || !tile._isEmpty) 
            {
                return false;
            }
        }
        return true;
    }
}