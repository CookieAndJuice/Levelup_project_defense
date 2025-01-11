using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    public GameObject TitleGroup;       // 메인 타이틀 UI묶음
    public GameObject GameSettingGroup;         // 게임 세팅 UI묶음
    public string SceneToLoad;      // 게임 시작 시 나오는 인게임 Scene
    public GameObject Creator; // 제작자 UI

    public Button StartGameBtn;     // 게임 시작 버튼
    public Button GameSettingBtn;       // 환경 설정 버튼
    public Button ExitGameBtn;      // 게임 종료 버튼
    public Button CreatorBtn; // 제작자 버튼

    GameObject SM;
    private void Awake()
    {
        SM = GameObject.Find("SoundManager");
    }
    void Start()
    {
        StartGameBtn.onClick.AddListener(ClickToStart);     // 세 버튼에 각 함수 추가
        GameSettingBtn.onClick.AddListener(ClickToSet);
        ExitGameBtn.onClick.AddListener(ClickToExit);
        CreatorBtn.onClick.AddListener(ClickToCreator);
    }

    public void ClickToStart()      // 인 게임 Scene으로 씬 변경
    {
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
        SceneManager.LoadScene(SceneToLoad);
    }

    public void ClickToSet()        // 환경 설정 UI 활성화, 메인 타이틀 UI 비활성화
    {
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
        GameSettingGroup.SetActive(true);
        TitleGroup.SetActive(false);
    }

    public void ClickToExit()       // 게임 종료
    {
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
        Application.Quit();
    }

    public void ClickToCreator()
    {
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
        Creator.SetActive(true);
    }

    public void ExitCreator()
    {
        Creator.SetActive(false);
    }
}
