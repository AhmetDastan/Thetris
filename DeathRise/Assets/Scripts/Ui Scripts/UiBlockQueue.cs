using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class UiBlockQueue : MonoBehaviour
{
    [SerializeField] internal Image[] blocksQueueSprite;
    [SerializeField] internal GameUiManager gameUiManager;

    private void Start()
    {
        
    }

    public void AddSpriteBlockQueue(string blockTag)
    {
        if (blockTag == null)
        {
            Debug.Log("BlockTag is null, Queue sprite didint update");
            return;
        } 
        foreach (var sprite in gameUiManager.blocksSprite)
        {  
            if (sprite.name == (blockTag + " Sprite"))
            {
                //find last image
                for (int i = 0; i < blocksQueueSprite.Length; i++)
                {
                    Debug.Log("ilki null degil mi " + blocksQueueSprite[i].sprite);
                    if (blocksQueueSprite[i].sprite == null)
                    {
                        blocksQueueSprite[i].sprite = sprite;
                        Debug.Log("heyyo nullda " + blocksQueueSprite[i].sprite.name);
                        break;
                    }else if( (i+1) == blocksQueueSprite.Length)
                    {
                        blocksQueueSprite[0].sprite = blocksQueueSprite[1].sprite;
                        blocksQueueSprite[1].sprite = blocksQueueSprite[2].sprite;
                        blocksQueueSprite[2].sprite = sprite;
                        Debug.Log("son eleman");
                        break;
                    }
                    Debug.Log("yok degil");
                }
                break;
            }
        }
        return;
    }
}
