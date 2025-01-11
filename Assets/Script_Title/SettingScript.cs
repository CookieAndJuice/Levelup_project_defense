using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public GameObject TitleGroup;       // 메인 타이틀 UI묶음
    public GameObject GameSettingGroup;         // 게임 세팅 UI묶음

    public Button ReturnTitleBtn;       // 타이틀로 돌아가는 버튼
    GameObject SM;
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.Find("SoundManager");
        ReturnTitleBtn.onClick.AddListener(ClickToReturn);      // ReturnTitleBtn에 함수 추가
    }

    public void ClickToReturn()     // 메인 타이틀 UI 활성화, 환경 설정 UI 비활성화
    {
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
        TitleGroup.SetActive(true);
        GameSettingGroup.SetActive(false);
    }
}