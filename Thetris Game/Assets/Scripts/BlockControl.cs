using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    public float blockSpeed = 1;

    [SerializeField] public GridManager gridManager;

    private void Start()
    {
        StartCoroutine(FallDown());
    }

    void Update()
    {
        BlockMovement();
    }

    void BlockMovement()
    {
            if (InputManager.isPressedLeft)
            {
                transform.position += new Vector3(-1, 0, 0);
                if(!ValidMove())
                    transform.position -= new Vector3(-1, 0, 0);
            }
            if (InputManager.isPressedRight)
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                    transform.position -= new Vector3(1, 0, 0);
            }
            if (InputManager.isPressedM)
            {
                transform.RotateAround(transform.position, Vector3.forward, 90);
                if (!ValidMove())
                    transform.RotateAround(transform.position, Vector3.back, 90);
            }
            if (InputManager.isPressedN)
            {
                transform.RotateAround(transform.position, Vector3.back, 90);
                if (!ValidMove())
                    transform.RotateAround(transform.position, Vector3.forward, 90);
            }
    }

    private IEnumerator FallDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(blockSpeed);
            transform.position -= new Vector3(0, 1, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
                yield break;
            }
        }
    }

    bool ValidMove()
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