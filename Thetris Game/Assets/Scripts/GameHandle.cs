using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{

    [SerializeField] BlockManager blockManager;

    internal bool oldBlockStopped = false;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (oldBlockStopped)
        {
            oldBlockStopped = false;
            blockManager.needNewBlock = true;
        }
    }
}
