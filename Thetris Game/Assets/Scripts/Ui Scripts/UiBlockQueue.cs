using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class UiBlockQueue : MonoBehaviour
{
    [SerializeField] private Image[] blocksQueueSprite;
    [SerializeField] internal GameUiManager gameUiManager;

    public void AddSpriteBlockQueue(string blockTag)
    {
        foreach (var sprite in gameUiManager.blocksSprite)
        {
            if (sprite.name == (blockTag + " Sprite"))
            {
                //find last image
                for (int i = 0; i < blocksQueueSprite.Length; i++)
                {
                    if(blocksQueueSprite[i] == null)
                    {
                        blocksQueueSprite[i].sprite = sprite;
                    }else if( (i+1) == blocksQueueSprite.Length)
                    {
                        blocksQueueSprite[0].sprite = blocksQueueSprite[1].sprite;
                        blocksQueueSprite[1].sprite = blocksQueueSprite[2].sprite;
                        blocksQueueSprite[2].sprite = sprite;
                    }
                }
                break;
            }
        }
    }
}
