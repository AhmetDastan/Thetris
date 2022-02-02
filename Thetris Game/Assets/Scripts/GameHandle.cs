using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
    [SerializeField] BlockSpawner blockSpawner;

    public int levelNum = 0;
    internal int currentFrame = 48;
    internal float currentScore = 0;
    internal bool oldBlockStopped = false;

    internal GameObject currentBlock;


    // Start is called before the first frame update
    void Start()
    {
        currentBlock = blockSpawner.SpawnBlock();
        levelNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (oldBlockStopped)
        {
            oldBlockStopped = false;
            currentBlock = blockSpawner.SpawnBlock();
            currentFrame = LevelConstant.getFrameAmount(levelNum);
        }
    }
}
