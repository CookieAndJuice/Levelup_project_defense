using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    public GameObject TitleGroup;       // ���� Ÿ��Ʋ UI����
    public GameObject GameSettingGroup;         // ���� ���� UI����
    public string SceneToLoad;      // ���� ���� �� ������ �ΰ��� Scene
    public GameObject Creator; // ������ UI

    public Button StartGameBtn;     // ���� ���� ��ư
    public Button GameSettingBtn;       // ȯ�� ���� ��ư
    public Button ExitGameBtn;      // ���� ���� ��ư
    public Button CreatorBtn; // ������ ��ư

    GameObject SM;
    private void Awake()
    {
        SM = GameObject.Find("SoundManager");
    }
    void Start()
    {
        StartGameBtn.onClick.AddListener(ClickToStart);     // �� ��ư�� �� �Լ� �߰�
        GameSettingBtn.onClick.AddListener(ClickToSet);
        ExitGameBtn.onClick.AddListener(ClickToExit);
        CreatorBtn.onClick.AddListener(ClickToCreator);
    }

    public void ClickToStart()      // �� ���� Scene���� �� ����
    {
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
        SceneManager.LoadScene(SceneToLoad);
    }

    public void ClickToSet()        // ȯ�� ���� UI Ȱ��ȭ, ���� Ÿ��Ʋ UI ��Ȱ��ȭ
    {
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
        GameSettingGroup.SetActive(true);
        TitleGroup.SetActive(false);
    }

    public void ClickToExit()       // ���� ����
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
