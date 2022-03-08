using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameHandle gameHandle;
    [SerializeField] private GameObject[] blockPrefabs;

    [SerializeField] private Queue<GameObject> comingBlockQueue = new Queue<GameObject>();

    GameObject tempGo;

    public GameObject SpawnBlock()
    {
        if (comingBlockQueue.Count < 4)
        {
            FillBlockQueue();
        }
        tempGo = comingBlockQueue.Dequeue();
        tempGo.SetActive(true);
        return tempGo;
    }

    void FillBlockQueue()
    {
        while (!(comingBlockQueue.Count >= 4))
        {
            tempGo = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], new Vector3(5f, 18f, 0), Quaternion.identity);
            tempGo.SetActive(false);
            comingBlockQueue.Enqueue(tempGo);
            gameHandle.gameUiManager.uiBlockQueue.AddSpriteBlockQueue(tempGo.tag);
        }
    }
}
