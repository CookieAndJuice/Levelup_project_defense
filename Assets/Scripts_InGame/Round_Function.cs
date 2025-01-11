using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round_Function : MonoBehaviour
{
    public int round;
    public Text round_text;
    void Start()
    {
        round = 1;
        round_text.text = "Round 1";
    }

    void Update()
    {
        
    }
}
