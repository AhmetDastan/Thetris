using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldBlock : MonoBehaviour
{
    internal GameObject blockInHold;
    internal GameObject tempGo;
    

    [SerializeField] internal GameUiManager gameUiManager;

    [SerializeField] internal bool isHoldAreaEmpty = true;
    [SerializeField] internal bool replaceable = true;

    [SerializeField] private Image holdBlockImage;

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
            gameUiManager.gameHandle.isNeedNewBlock = true;
        } else if (replaceable)
        {
            replaceable = false;

            SwitchBtwHoldAndHand();
        }
    }

    void SetBlocktoHold()
    {
        gameUiManager.gameHandle.currentBlock.SetActive(false);
        blockInHold = gameUiManager.gameHandle.currentBlock;
        blockInHold = gameUiManager.gameHandle.currentBlock;
        ChangeHoldButtonImage();
    }

    void SwitchBtwHoldAndHand()
    {
        tempGo = blockInHold;
        SetBlocktoHold();
        gameUiManager.gameHandle.currentBlock = tempGo;
        gameUiManager.gameHandle.currentBlock.SetActive(true);
        gameUiManager.gameHandle.currentBlock.transform.position = new Vector2(5, 18);
        ChangeHoldButtonImage();
    }

    private void ChangeHoldButtonImage()
    {
        foreach (var sprite in gameUiManager.blocksSprite)
        {
            if (sprite.name == (blockInHold.tag + " Sprite")) 
            {
                holdBlockImage.sprite = sprite;
                break;
            }
        }
    }

}
