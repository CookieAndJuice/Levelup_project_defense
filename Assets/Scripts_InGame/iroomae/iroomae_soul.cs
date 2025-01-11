using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iroomae_soul : MonoBehaviour
{
    void Start()
    {
        
    }

    private void Update()
    {
        transform.position = transform.position + Vector3.up * Time.deltaTime * 4;
        Destroy(gameObject, 2.5f);
    }


}
