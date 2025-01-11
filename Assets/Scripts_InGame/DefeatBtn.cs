using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DefeatBtn : MonoBehaviour
{


    public void Run()       // 빤스런 버튼
    {
        AudioPlayer.instance.PlayButtonClip();
        SceneManager.LoadScene("StartScene");
    }

    public void Restart()        // 종료 취소 버튼
    {
        AudioPlayer.instance.PlayButtonClip();
        SceneManager.LoadScene("IngameScene");
    }
}
