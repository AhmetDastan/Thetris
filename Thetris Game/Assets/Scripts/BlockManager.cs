using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockManager : MonoBehaviour
{
    public BlockSpawner blockSpawner;
    public bool needNewBlock = false;
    public GameObject currentBlock;
    
    void Start()
    {
        needNewBlock = false;
        currentBlock = blockSpawner.SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (needNewBlock) 
        {
            needNewBlock = false;
            currentBlock = blockSpawner.SpawnBlock();
        }
    }
}
