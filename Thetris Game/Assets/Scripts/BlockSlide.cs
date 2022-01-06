using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlide : MonoBehaviour
{
    public float blockSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveDown()) ;
    }

    // Update is called once per frame
   /* void Update()
    {
        if (waitingTime > 0) 
        {
            waitingTime -= Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.down);
            waitingTime = 1;
        }
    }*/

    private IEnumerator MoveDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(blockSpeed);
            Vector3 position = transform.position;
            position.y--;
            transform.position = position;
            if(transform.position.y <= -9.5f)
            {
                yield break;
            }
        }
    }
}
