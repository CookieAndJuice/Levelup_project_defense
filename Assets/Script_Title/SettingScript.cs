using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public GameObject TitleGroup;       // ���� Ÿ��Ʋ UI����
    public GameObject GameSettingGroup;         // ���� ���� UI����

    public Button ReturnTitleBtn;       // Ÿ��Ʋ�� ���ư��� ��ư
    GameObject SM;
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.Find("SoundManager");
        ReturnTitleBtn.onClick.AddListener(ClickToReturn);      // ReturnTitleBtn�� �Լ� �߰�
    }

    public void ClickToReturn()     // ���� Ÿ��Ʋ UI Ȱ��ȭ, ȯ�� ���� UI ��Ȱ��ȭ
    {
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
        TitleGroup.SetActive(true);
        GameSettingGroup.SetActive(false);
    }
}