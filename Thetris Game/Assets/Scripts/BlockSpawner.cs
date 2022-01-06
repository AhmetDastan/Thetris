﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*block havuzundan bi blok sec eger sectigin yerde block yoksa yeni blok olustur
  yok olan blocklari blok havuzuna ekle 
   block spawn ile iligli her turlu isi burada yap 
 */


public class BlockSpawner : MonoBehaviour
{
    public BlockManager blockManager;
    [SerializeField] private GameObject[] blockPrefabs;

    private void Start()
    {
        Instantiate(blockPrefabs[0], new Vector2(5f, 19.5f), Quaternion.identity);
    }
    public GameObject SpawnBlock()
    {
        GameObject currentBlock = Instantiate(blockPrefabs[0], new Vector2(5f, 17f), Quaternion.identity);
        return currentBlock;
    }
}
