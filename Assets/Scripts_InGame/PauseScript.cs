using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject ExitScreen;

    public static Button PauseBtn;
    public Button ResumeBtn;
    public Button ExitBtn;
    public Button ExitYesBtn;
    public Button ExitNoBtn;

    public string LoadToTitle;      // 타이틀 씬 문자열

    public static bool PauseActive;

    void Awake()
    {
        Time.timeScale = 1;
        PauseBtn = GameObject.Find("PauseBtn").GetComponent<Button>();
    }
    private void Start()
    {
        PauseBtn.onClick.AddListener(ClickToPause);
        ResumeBtn.onClick.AddListener(ClickToResume);
        ExitBtn.onClick.AddListener(ClickExitBtn);
        ExitYesBtn.onClick.AddListener(ExitYesFunc);
        ExitNoBtn.onClick.AddListener(ExitNoFunc);

        PauseActive = false;
    }

    public void ClickToPause()      // 게임 시간 멈춤. 일시 정지 화면 표시. 여러 버튼 비활성화
    {
        AudioPlayer.instance.PlayButtonClip();
        RoundAhead.SpeedUpBtn.interactable = false;
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
        PauseActive = true;

        Click_Function.GambleBtn.interactable = false;
        for (int i = 0; i < 4; i++)
        {
            Click_Function.CharacterBtn[i].interactable = false;
        }
    }

    public void ClickToResume()     // 게임 시간 다시 진행. 일시 정지 화면 비표시. 여러 버튼 활성화
    {
        Time.timeScale = RoundAhead.SpeedBtnValue;
        AudioPlayer.instance.PlayButtonClip();
        RoundAhead.SpeedUpBtn.interactable = true;
        PauseScreen.SetActive(false);
        PauseActive = false;

        Click_Function.GambleBtn.interactable = true;
        for (int i = 0; i < 4; i++)
        {
            Click_Function.CharacterBtn[i].interactable = true;
        }
    }

    public void ClickExitBtn()      // 일시 정지 창의 종료 버튼
    {
        Time.timeScale = 1;
        AudioPlayer.instance.PlayButtonClip();
        Time.timeScale = 0;
        ExitScreen.SetActive(true);
    }

    public void ExitYesFunc()       // 종료 확인 버튼
    {
        Time.timeScale = 1;
        AudioPlayer.instance.PlayButtonClip();
        SceneManager.LoadScene(LoadToTitle);
    }

    public void ExitNoFunc()        // 종료 취소 버튼
    {
        Time.timeScale = 1;
        AudioPlayer.instance.PlayButtonClip();
        Time.timeScale = 0;
        ExitScreen.SetActive(false);
    }
}
