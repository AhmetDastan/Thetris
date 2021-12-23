using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlide : MonoBehaviour
{
    public float speed = 1f;
    private float waitingTime = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
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
    }
}
