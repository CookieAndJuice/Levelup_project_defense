using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed;
    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.left*speed*Time.deltaTime);
        if(transform.position.x <= -33f)
        {
            transform.position = new Vector3(56, 1.1f, 10.5f);
        }
    }
}
