using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_position : MonoBehaviour
{
    public Transform obj;
    public GameObject hp_bar;


    void Start()
    {
        if(obj != null)
        {
            hp_bar.transform.position = obj.position;
        }
    }

    void Update()
    {
        if(obj != null)
        {
            hp_bar.transform.position = obj.position + new Vector3(0, 2.0f, 0);
        }
    }
}
