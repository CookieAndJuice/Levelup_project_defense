using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DefeatBtn : MonoBehaviour
{


    public void Run()       // ������ ��ư
    {
        AudioPlayer.instance.PlayButtonClip();
        SceneManager.LoadScene("StartScene");
    }

    public void Restart()        // ���� ��� ��ư
    {
        AudioPlayer.instance.PlayButtonClip();
        SceneManager.LoadScene("IngameScene");
    }
}
