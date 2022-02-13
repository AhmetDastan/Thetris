using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldBlock : MonoBehaviour
{
    internal GameObject blockInHold;
    internal GameObject tempGo;

    private Button holdButton;

    [SerializeField] internal GameHandle gameHandle;

    [SerializeField] internal bool isHoldAreaEmpty = true;
    [SerializeField] internal bool replaceable = true;

    // Start is called before the first frame update
    void Start()
    {
        isHoldAreaEmpty = true;
        replaceable = false;
        
    }

    public void SwichBlock()
    {
        if (isHoldAreaEmpty)
        {
            isHoldAreaEmpty = false;

            SetBlocktoHold();
            gameHandle.isNeedNewBlock = true;
        }else if (replaceable)// her yeni turda bir hakkin var swich icin
        {
            replaceable = false;

            SwitchBtwHoldAndHand();
        }
    }

    void SetBlocktoHold()
    {

        gameHandle.currentBlock.SetActive(false);
        gameHandle.currentBlock.transform.position = new Vector2(40, 40);
        blockInHold = gameHandle.currentBlock;
    }

    void SwitchBtwHoldAndHand()
    {
        tempGo = blockInHold;
        SetBlocktoHold();
        gameHandle.currentBlock = tempGo;
        gameHandle.currentBlock.SetActive(true);
        gameHandle.currentBlock.transform.position = new Vector2(5, 18);
    }
}
