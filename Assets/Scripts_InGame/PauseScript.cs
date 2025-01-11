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

    public string LoadToTitle;      // Ÿ��Ʋ �� ���ڿ�

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

    public void ClickToPause()      // ���� �ð� ����. �Ͻ� ���� ȭ�� ǥ��. ���� ��ư ��Ȱ��ȭ
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

    public void ClickToResume()     // ���� �ð� �ٽ� ����. �Ͻ� ���� ȭ�� ��ǥ��. ���� ��ư Ȱ��ȭ
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

    public void ClickExitBtn()      // �Ͻ� ���� â�� ���� ��ư
    {
        Time.timeScale = 1;
        AudioPlayer.instance.PlayButtonClip();
        Time.timeScale = 0;
        ExitScreen.SetActive(true);
    }

    public void ExitYesFunc()       // ���� Ȯ�� ��ư
    {
        Time.timeScale = 1;
        AudioPlayer.instance.PlayButtonClip();
        SceneManager.LoadScene(LoadToTitle);
    }

    public void ExitNoFunc()        // ���� ��� ��ư
    {
        Time.timeScale = 1;
        AudioPlayer.instance.PlayButtonClip();
        Time.timeScale = 0;
        ExitScreen.SetActive(false);
    }
}
