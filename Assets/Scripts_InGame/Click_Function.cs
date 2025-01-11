using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_Function : MonoBehaviour
{
    public GameObject[] Job = new GameObject[4];  // 직업 배열


    public static Button[] CharacterBtn = new Button[4];   // 직업 별 업그레이드 버튼
    public static Button GambleBtn;    // 뽑기 버튼

    public Text[] GoldText = new Text[4];   // 직업 별 업그레이드 금액 텍스트
    public Text CharacterBtnText;      // 임의의 직업 버튼 텍스트

    public Image CharacterImage;

    public static int charnum;  // 캐릭터 개수
    public static int GambleGold;  // 뽑기 비용
    public static int[] UpgradeGold = new int[4];   // 직업 별 현재 업그레이드 금액
    public static int UpGoldStd1 = 200;   // 1번째 업그레이드 골드량 기준
    public static int UpGoldStd2 = 500;   // 2번째 업그레이드 골드량 기준

    int JobNum;



    Vector3 iroomae_initpos = new Vector3(-6.58f, -2.67f, 0);
    Vector3 enemy_initpos = new Vector3(28.61f, -3.11f, 0);
    Vector3 tanker2_initpos = new Vector3(-6.58f, -2.4f, 0);
    Vector3 tanker3_initpos = new Vector3(-6.58f, -2.05f, 0);

    public GameObject c_e_prefab;
    public GameObject f_e_prefab;

    public Button HelpBtn;

    // 2,3 단계 프리팹 정의
    GameObject t_2;
    GameObject t_3;

    GameObject s_2;
    GameObject s_3;

    GameObject g_2;
    GameObject g_3;

    GameObject m_2;
    GameObject m_3;

    // 변경할 이미지 모음
    public Sprite[] change_img = new Sprite[8];
    // 변경할 색상
    public Color rare;
    public Color epic;
    public Image[] img = new Image[4];

    GameObject SM;

    private void Awake()
    {
        SM = GameObject.Find("SoundManager");
        for (int i = 0; i < 4; i++)
        {
            UpgradeGold[i] = 200;
            CharacterBtn[i] = GameObject.Find("GambleUI").transform.GetChild((i+1) * 2).GetComponent<Button>();
            //img[i] = transform.GetComponent<Image>();
        }

        GambleBtn = GameObject.Find("GambleButton").GetComponent<Button>();
        GambleBtn.onClick.AddListener(OnClick);
        CharacterBtn[0].onClick.AddListener(TankClick);
        CharacterBtn[1].onClick.AddListener(SwordsmanClick);
        CharacterBtn[2].onClick.AddListener(GunnerClick);
        CharacterBtn[3].onClick.AddListener(MagicianClick);

        // Resources 에서 프리팹 로드
        t_2 = Resources.Load<GameObject>("prefab/Iroomae_prefab/tanker/iroomae_tanker2");
        t_3 = Resources.Load<GameObject>("prefab/Iroomae_prefab/tanker/iroomae_tanker3");

        s_2 = Resources.Load<GameObject>("prefab/Iroomae_prefab/swordman/iroomae_swordman2");
        s_3 = Resources.Load<GameObject>("prefab/Iroomae_prefab/swordman/iroomae_swordman3");

        g_2 = Resources.Load<GameObject>("prefab/Iroomae_prefab/gunner/iroomae_gunner2");
        g_3 = Resources.Load<GameObject>("prefab/Iroomae_prefab/gunner/iroomae_gunner3");

        m_2 = Resources.Load<GameObject>("prefab/Iroomae_prefab/magician/iroomae_magician2");
        m_3 = Resources.Load<GameObject>("prefab/Iroomae_prefab/magician/iroomae_magician3");
    }
    private void Update()
    {
        if (RoundAhead.isinteractable == true)
        {
            if (PauseScript.PauseActive == false)
            {
                HelpBtn.interactable = true;
                PauseScript.PauseBtn.interactable = true;
                if (GoldScript.Gold < 50)
                {
                    GambleBtn.interactable = false;
                }
                else
                {
                    GambleBtn.interactable = true;
                }
            }
            else
            {
                PauseScript.PauseBtn.interactable = false;
                GambleBtn.interactable = false;
                HelpBtn.interactable = false;
            }

        }
    }


    public void TankClick()
    {
        if (GoldScript.Gold >= UpgradeGold[0] && UpgradeGold[0] == UpGoldStd1)
        {
            // UI 바꾸는 코드
            CharacterImage = GameObject.Find("Tanker_img").GetComponentInChildren<Image>();
            CharacterImage.sprite = change_img[0];
            // UI 색변경
            img[0].color = rare;

            Job[0] = t_2;     // 첫번째 업그레이드 시 프리팹 교체 -> 프리팹이 없어서 교체 안 하고 주석처리함

            UpgradeGold[0] = UpGoldStd2;        // 해당 직업 업그레이드 골드량 250으로 올림
            GoldText[0].text = UpgradeGold[0].ToString();  // 필요 골드량 표시 바꾸기
            GoldScript.Gold -= UpGoldStd1;      // 소비한 골드 빼주기
        }
        else if (GoldScript.Gold >= UpgradeGold[0] && UpgradeGold[0] == UpGoldStd2)
        {
            CharacterImage = GameObject.Find("Tanker_img").GetComponentInChildren<Image>();
            CharacterImage.sprite = change_img[1];
            // UI 색변경
            img[0].color = epic;

            Job[0] = t_3;      // 두번째 업글 시 프리팹 교체

            UpgradeGold[0] = 0;
            GoldText[0].text = "Max";
            GoldScript.Gold -= UpGoldStd2;

            CharacterBtn[0].interactable = false;
        }
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
    }

    public void SwordsmanClick()
    {
        if (GoldScript.Gold >= UpgradeGold[1] && UpgradeGold[1] == UpGoldStd1)
        {
            // UI 바꾸는 코드
            CharacterImage = GameObject.Find("Swordman_img").GetComponentInChildren<Image>();
            CharacterImage.sprite = change_img[2];
            // UI 색변경
            img[1].color = rare;

            Job[1] = s_2;      // 첫번째 업그레이드 시 프리팹 교체

            UpgradeGold[1] = UpGoldStd2;
            GoldText[1].text = UpgradeGold[1].ToString();
            GoldScript.Gold -= UpGoldStd1;
        }
        else if (GoldScript.Gold >= UpgradeGold[1] && UpgradeGold[1] == UpGoldStd2)
        {
            // UI 바꾸는 코드
            CharacterImage = GameObject.Find("Swordman_img").GetComponentInChildren<Image>();
            CharacterImage.sprite = change_img[3];
            // UI 색변경
            img[1].color = epic;

            Job[1] = s_3;      // 두번째 업글 시 프리팹 교체

            UpgradeGold[1] = 0;
            GoldText[1].text = "Max";
            GoldScript.Gold -= UpGoldStd2;

            CharacterBtn[1].interactable = false;
        }
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
    }

    public void GunnerClick()
    {
        if (GoldScript.Gold >= UpgradeGold[2] && UpgradeGold[2] == UpGoldStd1)
        {
            // UI 바꾸는 코드
            CharacterImage = GameObject.Find("Gunner_img").GetComponentInChildren<Image>();
            CharacterImage.sprite = change_img[4];
            // UI 색변경
            img[2].color = rare;

            Job[2] = g_2;      // 첫번째 업그레이드 시 프리팹 교체

            UpgradeGold[2] = UpGoldStd2;
            GoldText[2].text = UpgradeGold[2].ToString();
            GoldScript.Gold -= UpGoldStd1;
        }
        else if (GoldScript.Gold >= UpgradeGold[2] && UpgradeGold[2] == UpGoldStd2)
        {
            // UI 바꾸는 코드
            CharacterImage = GameObject.Find("Gunner_img").GetComponentInChildren<Image>();
            CharacterImage.sprite = change_img[5];
            // UI 색변경
            img[2].color = epic;

            Job[2] = g_3;     // 두번째 업글 시 프리팹 교체

            UpgradeGold[2] = 0;
            GoldText[2].text = "Max";
            GoldScript.Gold -= UpGoldStd2;

            CharacterBtn[2].interactable = false;
        }
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
    }

    public void MagicianClick()
    {
        if (GoldScript.Gold >= UpgradeGold[3] && UpgradeGold[3] == UpGoldStd1)
        {
            // UI 바꾸는 코드
            CharacterImage = GameObject.Find("Magician_img").GetComponentInChildren<Image>();
            CharacterImage.sprite = change_img[6];
            // UI 색변경
            img[3].color = rare;

            Job[3] = m_2;      // 첫번째 업그레이드 시 프리팹 교체

            UpgradeGold[3] = UpGoldStd2;
            GoldText[3].text = UpgradeGold[3].ToString();
            GoldScript.Gold -= UpGoldStd1;
        }
        else if (GoldScript.Gold >= UpgradeGold[3] && UpgradeGold[3] == UpGoldStd2)
        {
            // UI 바꾸는 코드
            CharacterImage = GameObject.Find("Magician_img").GetComponentInChildren<Image>();
            CharacterImage.sprite = change_img[7];
            // UI 색변경
            img[3].color = epic;

            Job[3] = m_3;    // 두번째 업글 시 프리팹 교체

            UpgradeGold[3] = 0;
            GoldText[3].text = "Max";
            GoldScript.Gold -= UpGoldStd2;

            CharacterBtn[3].interactable = false;
        }
        SM.GetComponent<AudioPlayer>().PlayButtonClip();
    }



    public void OnClick()       // 해당 직업 캐릭터 추가하는 함수
    {
        if (GoldScript.Gold >= 50)  // 현재gold가 50gold 이상이여야만 뽑기 가능
        {
            GoldScript.Gold -= 50;  // 현재gold - 50gold
            JobNum = Random.Range(0, 100) % 4;

            // 탱커 y축 위치 조정
            if(JobNum == 0)
            {
                if (Job[JobNum] == t_2)
                {
                    Instantiate(Job[JobNum], tanker2_initpos, Quaternion.identity, GameObject.Find("MyCharacters").transform);
                }
                else if (Job[JobNum] == t_3)
                {
                    Instantiate(Job[JobNum], tanker3_initpos, Quaternion.identity, GameObject.Find("MyCharacters").transform);
                }
                else
                {

                    Instantiate(Job[JobNum], iroomae_initpos, Quaternion.identity, GameObject.Find("MyCharacters").transform);
                }

            }
            else
            {
                Instantiate(Job[JobNum], iroomae_initpos, Quaternion.identity, GameObject.Find("MyCharacters").transform);
            }
            charnum++;
            SM.GetComponent<AudioPlayer>().PlayButtonClip();
        }
    }

    

}
