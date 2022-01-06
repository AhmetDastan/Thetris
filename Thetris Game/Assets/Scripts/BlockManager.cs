﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockManager : MonoBehaviour
{
    /*
        yeni block ihtiyacin olursa blockspawnera bildir
        blocklarin hiz gibi degisen ozellikleri senin uzerinden yurutulsun
        
     */
    public BlockSpawner blockSpawner;
    public bool needNewBlock = false;
    private GameObject currentBlock;
    
    // Start is called before the first frame update
    void Start()
    {
        //Tilemap tilemap;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (needNewBlock) 
        {
            needNewBlock = false;
            currentBlock = blockSpawner.SpawnBlock();
            Debug.Log(currentBlock);
        }
    }
}
