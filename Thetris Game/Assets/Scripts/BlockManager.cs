using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockManager : MonoBehaviour
{
    /*
        yeni block ihtiyacin olursa blockspawnera bildir
        blocklarin hiz gibi degisen ozellikleri senin uzerinden yurutulsun
        
     */
    public bool needNewBlock = false;

    [SerializeField] private GameObject[] blockPrefabs;
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
            Instantiate(blockPrefabs[1], new Vector2(-0.5f,8.5f) , Quaternion.identity);
            needNewBlock = false;
        }
    }
}
