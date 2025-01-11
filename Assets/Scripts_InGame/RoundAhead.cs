using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;


// 적 포탈이 초마다 알아서 피가 깎이도록 만들어 놓음.
// 

public class RoundAhead : MonoBehaviour
{
    //public static int EnemyPortalHP = 100;        // 포탈 현재 hp
    //public static int EnemyPortalMaxHP = 100;        // 포탈 max hp
    public static int RoundNum;     // 라운드 수
    public string[] RoundName = new string[5];
    public static float StartTimer = 3f;      // 시작 타이머
    public string TitleScene;       // 메인메뉴 씬

    public static bool On = true;
    public static bool Off = false;

    public Transform MyCharacters;      // 아군 캐릭터들 폴더 오브젝트
    public Transform Monsters;          // 적군 캐릭터들 폴더 오브젝트
    public GameObject Portal;
    public GameObject StartUI;        // 라운드 시작 시 나오는 UI
    public GameObject FinishUI;       // 라운드 종료 시 나오는 UI

    public GameObject FinishBtn;      // 게임 클리어 후 메인화면으로 가는 버튼

    public Text RoundText;      // 라운드 수 적는 텍스트
    public Text RoundStartText;         // 라운드 시작 시 나오는 텍스트
    public Text RoundFinishText;        // 라운드 종료 시 나오는 텍스트

    // 이하 적 생성에 관한 변수들
    public static float WaveTerm_Seconds = 20f;      // 웨이브 간격 20초
    public static int WaveNum = 3;          // 라운드 별 웨이브 수 3개
    public static int WaveEnemiesNum = 7;       // 1웨이브 당 몬스터 수 7마리씩

    // 적 오브젝트 종류 별로 저장할 배열
    public GameObject[] Enemies = new GameObject[8];
    // index - 0,1 : 1라운드 / index - 2,3 : 2라운드 / ... 이런 식
    // index - 짝수 : 근접 / 홀수 : 원거리

    public GameObject SpecialMob;       // 미대 스페셜 몬스터
    public GameObject MiddleBoss;       // 중간 보스
    public GameObject FinalBoss;        // 최종 보스

    public static GameObject Enemy;        // 적 오브젝트 임의 변수
    public static GameObject SpEnemy;       // 스페셜 적 오브젝트 임의 변수 -> 비효율적이긴 함
    public static GameObject[] WaveEnemies = new GameObject[7];        // 한 웨이브에 나올 적들 순서대로 저장해 놓은 배열

    public static int EnemyRandom = 0;        // 적 종류 고르는 데에 사용될 랜덤 변수
    public static int Melee = 0;          // 1웨이브 당 근접 공격인 적 수
    public static int Ranged = 0;         // 1웨이브 당 원거리 공격인 적 수
    public static int WaveEnemyRandom = 0;      // 웨이브마다 특정 라운드의 적들끼리 구분하기 위해 주어지는 난수
    public static int WaveMelee = 0;        // Enemies배열에 사용할 근접 적의 숫자
    public static int WaveRanged = 1;       // Enemies배열에 사용할 원거리 적의 숫자
    public static float SpEnemyRandom = 0;        // 스페셜 적 나오는 주기(난수)

    public static bool isinteractable = false;

    // 포탈 체력 관련
    VariablesManager variables;
    public GameObject portal;
    public GameObject boss;
    public enemy portalHPControl;
    public enemy bossHP;



    //도움말 관련
    public bool HelpUI_isFirst = true;
    public GameObject HelpUI;           // 게임 시작 전 나오는 도움말 UI ---------------
    public static bool HelpUI_Bool = true;       // HelpUI가 켜져야 하는지 나타내는 변수 ---------------

    public Button HelpUI_CloseBtn;       // HelpUI 창 닫기 버튼 ---------------
    public Button HelpUI_OpenBtn;           // HelpUI 창 열기 버튼 ---------------

    bool iscorutinestopped = false;

    // 마지막 라운드 관련
    public string[] LastMssgStartArray = new string[4];         // 마지막 라운드 시작 시 텍스트
    public string[] LastMssgFinishArray = new string[3];        // 마지막 라운드 종료 시 텍스트
    public int CharPerSeconds;          // 텍스트 출력 속도

    public GameObject LastRoundMssgBox;      // 마지막 라운드 텍스트 UI
    public GameObject Iroomae;     // 이루매 초상화
    public GameObject Boss;        // 교수님 초상화
    public GameObject EnterCursor;          // 대화 터치 커서

    public Text LastRoundMssgText;           // 마지막 라운드 텍스트
    public Sprite DownedBoss;
    public Image BossImage;



    // 배속 관련
    public static float SpeedBtnValue = 1f;      // 게임 빨리감기 상태를 나타낸 값 | 1 : 1배속, 1.5 : 1.5배속, 2 : 2배속

    public static Button SpeedUpBtn;           // 빨리감기 버튼

    public Text SpeedUpText;        // 빨리감기 버튼의 텍스트


    //이동 제한 관련
    public static bool isunitstop;

    private void Awake()
    {
        RoundNum = 1;
        GameObject VM = GameObject.Find("Variables_Manager");
        variables = VM.GetComponent<VariablesManager>();
        portalHPControl = portal.GetComponent<enemy>();
        bossHP = boss.GetComponent<enemy>();

    }
    void Start()
    {
        isunitstop = false;
        FinishBtn.GetComponent<Button>().onClick.AddListener(FinishBtnFunc);        // 메인메뉴 가는 함수 버튼에 할당

        // 라운드 텍스트 변수 초기화
        RoundText.text = RoundName[RoundNum-1];

        // gamble 버튼, 일시정지 버튼 비활성화
        RoundAhead.isinteractable = false;
        Click_Function.GambleBtn.interactable = false;
        PauseScript.PauseBtn.interactable = false;
        HelpUI_OpenBtn.interactable = false;

        // 게임 도움말 창
        HelpUI_CloseBtn.onClick.AddListener(CloseHelpUIFunc);
        HelpUI_OpenBtn.onClick.AddListener(OpenHelpUIFunc);
        OpenHelpUIFunc();

        SpeedUpBtn = GameObject.Find("SpeedUpBtn").GetComponent<Button>();          // static 변수라서 따로 할당해 줌

        SpeedUpBtn.onClick.AddListener(SpeedUpFunc);        // 빨리감기 버튼에 함수 할당

        SpeedUpBtn.interactable = false;

        for (int i = 0; i < 4; i++)
        {
            Click_Function.CharacterBtn[i].interactable = false;
        }

        SpeedBtnValue = 1f;

        // 라운드 시작까지 3초 남음
        //StartUI.SetActive(true);
        //RoundStartText.text = "Round " + RoundNum;

        //StartCoroutine(RoundStart(StartTimer));// 라운드 시작 코루틴

    }

    void Update()
    {

        if (portalHPControl.e_HP <= 0 && RoundNum <= 4)
        {
            
            // 라운드 수 증가. 즉 이 함수에는 RoundNum이 2, 3, 4, 5가 될 것임.
            RoundNum += 1;
            // 포탈 체력 설정
            portalHPControl.max_HP = variables.portal_HP[RoundNum - 1];
            portalHPControl.e_HP = portalHPControl.max_HP;

            SpeedUpBtn.interactable = false;
            // 사라지기 전에 포탈 폭발같은 애니메이션 있으면 좋겠음
            Portal.SetActive(false);        // 포탈 사라짐

            // 모든 캐릭터들 멈춤. 공격 X, 이동 X, 캐릭터 생성 X, 일시정지 X
            /// 짜야됨 ------------------------------------------------------------------------------
            /// 

            isinteractable = false;
            Click_Function.GambleBtn.interactable = false;
            PauseScript.PauseBtn.interactable = false;
            HelpUI_OpenBtn.interactable = false;

            FinishUI.SetActive(true);       // finishUI 나옴

            StopAllCoroutines();
            CancelInvoke("EnemyPortalFunc");
            GoldSwitch(Off);
            Invoke("NextRoundFunc", 5 * SpeedBtnValue);      // 5초 뒤 다음 라운드 시작
        }
        else if (RoundNum == 1 && HelpUI_isFirst == true && HelpUI_Bool == Off)       // 1라운드이고, 게임 시작 초기이고, HelpUI가 꺼졌다면
        {
            HelpUI_isFirst = false;

            StartUI.SetActive(true);
            RoundStartText.text = RoundName[RoundNum - 1];

            StartCoroutine(RoundStart(StartTimer));         // 라운드 시작 코루틴
        }
        else if(bossHP.e_HP <= 0 && RoundNum == 5)
        {
            foreach (Transform child in MyCharacters)   // 캐릭터, 몹 오브젝트 삭제
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in Monsters)
            {
                Destroy(child.gameObject);
            }
            isinteractable = false;
            Click_Function.GambleBtn.interactable = false;
            PauseScript.PauseBtn.interactable = false;
            HelpUI_OpenBtn.interactable = false;
            SpeedUpBtn.interactable = false;

            RoundNum += 1;
            StopAllCoroutines();
            GoldSwitch(Off);
            CancelInvoke("EnemyPortalFunc");

            StartCoroutine(FinishRoutine());
        }
    }

    public void NextRoundFunc()
    {

        RoundText.text = RoundName[RoundNum - 1];
        FinishUI.SetActive(false);      // finishUI 없앰

        if (RoundNum <= 4)       // 총 6라운드까지만 실행
        {

            foreach (Transform child in MyCharacters)   // 캐릭터, 몹 오브젝트 삭제
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in Monsters)
            {
                Destroy(child.gameObject);
            }


            // 라운드 시작까지 3초 남음
            StartUI.SetActive(true);
            RoundStartText.text = RoundName[RoundNum - 1];
            StartCoroutine(RoundStart(StartTimer));     // 라운드 시작 코루틴

            CancelInvoke("NextRoundFunc");
        }
        else if(RoundNum == 5)
        {
            StartCoroutine(LastRoundRoutine());
        }
    }

    IEnumerator LastRoundRoutine()           // 5라운드 초기화 코루틴
    {
        CancelInvoke("NextRoundFunc");


        foreach (Transform child in MyCharacters)   // 캐릭터, 몹 오브젝트 삭제
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in Monsters)
        {
            Destroy(child.gameObject);
        }


        boss.transform.position = new Vector3(28.19f, -2.6f, 0);
        Time.timeScale = 0;
        //isunitstop = true;

        // 텍스트 이펙트 구현(이루매 - 교수님 대화 장면)
        StringBuilder msgSB = new StringBuilder();          // stringbuilder 생성
        LastRoundMssgText.text = "";          // 대화 텍스트 초기화
        LastRoundMssgBox.SetActive(true);           // 대화 상자 ON
        Iroomae.SetActive(true);                // 이루매 대화 등장
        for (int i = 0; i < 4; i++)
        {
            EnterCursor.SetActive(false);
            msgSB.Clear();          // stringbuilder 초기화
            LastRoundMssgText.text = "";          // 대화 텍스트 초기화
            if (i == 1)
            {
                Iroomae.SetActive(false);                                       // 이루매 대화 퇴장
                LastRoundMssgText.alignment = TextAnchor.MiddleRight;           // 오른쪽 정렬
                Boss.SetActive(true);                                           // 교수님 대화 등장
            }

            for (int j = 0; j < LastMssgStartArray[i].Length; j++)          // 문장 타이핑 이펙트
            {
                msgSB.Append(LastMssgStartArray[i][j]);         // stringbuilder에 글자 하나씩 추가
                LastRoundMssgText.text = msgSB.ToString();
                yield return new WaitForSecondsRealtime(1.0f / CharPerSeconds);
            }

            EnterCursor.SetActive(true);            // 대화창 엔터 모양

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));          // 화면 터치해야 넘어감
        }
        //Time.timeScale = SpeedBtnValue;
        LastRoundMssgBox.SetActive(false);
        RoundStartText.text = "Final Round";
        StartUI.SetActive(true);

        StartCoroutine(RoundStart(StartTimer));     // 라운드 시작 코루틴

        StopCoroutine(LastRoundRoutine());
    }

    IEnumerator FinishRoutine()         // 게임 엔딩 코루틴
    {
        // 필드 위의 몹들 초기화
        foreach (Transform child in Monsters)       // 몹 오브젝트 삭제
        {
            Destroy(child.gameObject);
        }

        // 이미지를 쓰러진 교수님 이미지로 변환
        BossImage.sprite = DownedBoss;

        // 텍스트 이펙트 구현(교수님 최후 퇴장 장면)
        StringBuilder msgSB = new StringBuilder();
        LastRoundMssgText.text = string.Empty;          // 대화 텍스트 초기화
        LastRoundMssgBox.SetActive(true);           // 대화 상자 ON
        for (int i = 0; i < 3; i++)
        {
            EnterCursor.SetActive(false);
            msgSB.Clear();                                  // stringbuilder 초기화
            LastRoundMssgText.text = string.Empty;          // 대화 텍스트 초기화

            for (int j = 0; j < LastMssgFinishArray[i].Length; j++)
            {
                msgSB.Append(LastMssgFinishArray[i][j]);
                LastRoundMssgText.text = msgSB.ToString();
                yield return new WaitForSecondsRealtime(1.0f / CharPerSeconds);
            }

            EnterCursor.SetActive(true);

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        LastRoundMssgBox.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        boss.gameObject.GetComponent<enemy>().anim.SetInteger("state", 3);

        yield return new WaitForSeconds(3);
        boss.gameObject.SetActive(false);




        RoundFinishText.text = "축하합니다!!";
        FinishUI.SetActive(true);
        FinishBtn.SetActive(true);
    }

    IEnumerator RoundStart(float TimerN)            // 라운드 시작 코루틴
    {
        RoundStartText.text = RoundName[RoundNum - 1];
        yield return new WaitForSecondsRealtime(TimerN);        // TimerN만큼(3초) 기다림
        RoundStartText.text = "Start!";     // 방어 준비 -> 방어 시작! 으로 텍스트 변경
        
        SpeedUpBtn.interactable = true;
        // 1초 후 StartUI.SetActive(false), 
        yield return new WaitForSecondsRealtime(1.0f);
        StartUI.SetActive(false);

        if (RoundNum == 5)
            Time.timeScale = SpeedBtnValue;

        // 게임 시작 : 공격, 이동, 캐릭터 생성, 일시정지 모두 원상복귀
        //PlayerMobScript.UnitStop = false;
        //EnemyScript.UnitStop = false;                                       짜야됨-----------------------------------------------------------------

        isinteractable = true;
        Click_Function.GambleBtn.interactable = true;
        PauseScript.PauseBtn.interactable = true;
        HelpUI_OpenBtn.interactable = true;
        for (int i = 0; i < 4; i++)
        {
            Click_Function.CharacterBtn[i].interactable = true;
        }

        Portal.SetActive(true);     // 포탈 생성
        GoldSwitch(On);     // 골드 생성 시작

        // 적 생성 코드
        // Enemies 배열에 저장돼 있는 적 오브젝트들 중, 라운드에 맞게 랜덤으로 WaveEnemies 배열에 순서대로 저장함.
        // 한 웨이브마다 WaveEnemies 배열에 저장, 출력, 초기화 -> 총 3단계를 거침.
        switch (RoundNum)       // 라운드에 따라 적들이 다르게 생성되도록 함
        {
            case 1:         // 라운드 1
                for (int i = 0; i < WaveNum; i++)           // 웨이브 3개 동안 반복
                {
                    for (int j = 0; j < WaveEnemiesNum; j++)        // 1웨이브에 7마리가 나오고, 랜덤으로 근접2, 원거리2 이상 나오게 할당
                    {
                        if (j < 5)          // 5마리까지 랜덤으로 보냄
                        {
                            EnemyRandom = Random.Range(0, 100) % 2;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee >= 2 && Ranged >= 2)       // 6, 7번째는 근2 원2 조건을 맞추기 위해 if문으로 해결
                        {
                            EnemyRandom = Random.Range(0, 100) % 2;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee < 2)       // 6,7번째에서 조건 안될 경우 반복 없이 두 개 할당하고 반복문 종료
                        {
                            if (Melee == 0)
                            {
                                WaveEnemies[j] = Enemies[0];
                                WaveEnemies[j + 1] = Enemies[0];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[0];
                                EnemyRandom = Random.Range(0, 100) % 2;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }
                        else if (j >= 5 && Ranged < 2)       // 6,7번째에서 조건 안될 경우 반복 없이 두 개 할당하고 반복문 종료
                        {
                            if (Ranged == 0)
                            {
                                WaveEnemies[j] = Enemies[1];
                                WaveEnemies[j + 1] = Enemies[1];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[1];
                                EnemyRandom = Random.Range(0, 100) % 2;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }

                        if (EnemyRandom % 2 == 0)       // 근접, 원거리 개수 셈
                            Melee++;
                        else
                            Ranged++;
                    }

                    for (int k = 0; k < WaveEnemiesNum; k++)        // WaveEnemies 배열에 순서대로 저장된 적 오브젝트들 생성 코드
                    {
                        Enemy = Instantiate(WaveEnemies[k], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                };     // 라운드, 웨이브마다 다른 개체 수

                EnemyRandom = 0;        // 다음 라운드에서 재활용하기 위한 변수 초기화
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // 적 3마리씩 무한 생성
                {
                    for (int i = 0; i < 3; i++)          // 적 1마리씩 1초마다 소환, 3번 반복
                    {
                        EnemyRandom = Random.Range(0, 100) % 2;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                }

                break;
            case 2:
                for (int i = 0; i < WaveNum; i++)
                {
                    for (int j = 0; j < WaveEnemiesNum; j++)
                    {
                        if (j < 5)
                        {
                            EnemyRandom = Random.Range(0, 100) % 2 + 2;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee >= 2 && Ranged >= 2)
                        {
                            EnemyRandom = Random.Range(0, 100) % 2 + 2;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee < 2)
                        {
                            if (Melee == 0)
                            {
                                WaveEnemies[j] = Enemies[2];
                                WaveEnemies[j + 1] = Enemies[2];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[2];
                                EnemyRandom = Random.Range(0, 100) % 2 + 2;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }
                        else if (j >= 5 && Ranged < 2)
                        {
                            if (Ranged == 0)
                            {
                                WaveEnemies[j] = Enemies[3];
                                WaveEnemies[j + 1] = Enemies[3];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[3];
                                EnemyRandom = Random.Range(0, 100) % 2 + 2;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }

                        if (EnemyRandom % 2 == 0)
                            Melee++;
                        else
                            Ranged++;
                    }

                    for (int k = 0; k < WaveEnemiesNum; k++)
                    {
                        Enemy = Instantiate(WaveEnemies[k], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                };     // 라운드, 웨이브마다 다른 개체 수

                EnemyRandom = 0;
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // 적 3마리씩 무한 생성
                {
                    for (int i = 0; i < 3; i++)          // 적 1마리씩 1초마다 소환, 3번 반복
                    {
                        EnemyRandom = Random.Range(0, 100) % 2 + 2;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                }

                break;
            
            case 3:
                for (int i = 0; i < WaveNum; i++)
                {
                    for (int j = 0; j < WaveEnemiesNum; j++)
                    {
                        if (j < 5)
                        {
                            EnemyRandom = Random.Range(0, 100) % 2 + 4;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee >= 2 && Ranged >= 2)
                        {
                            EnemyRandom = Random.Range(0, 100) % 2 + 4;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee < 2)
                        {
                            if (Melee == 0)
                            {
                                WaveEnemies[j] = Enemies[4];
                                WaveEnemies[j + 1] = Enemies[4];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[4];
                                EnemyRandom = Random.Range(0, 100) % 2 + 4;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }
                        else if (j >= 5 && Ranged < 2)
                        {
                            if (Ranged == 0)
                            {
                                WaveEnemies[j] = Enemies[5];
                                WaveEnemies[j + 1] = Enemies[5];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[5];
                                EnemyRandom = Random.Range(0, 100) % 2 + 4;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }

                        if (EnemyRandom % 2 == 0)
                            Melee++;
                        else
                            Ranged++;
                    }

                    for (int k = 0; k < WaveEnemiesNum; k++)
                    {
                        Enemy = Instantiate(WaveEnemies[k], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                };     // 라운드, 웨이브마다 다른 개체 수

                EnemyRandom = 0;
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // 적 3마리씩 무한 생성
                {
                    for (int i = 0; i < 3; i++)          // 적 1마리씩 1초마다 소환, 3번 반복
                    {
                        EnemyRandom = Random.Range(0, 100) % 2 + 4;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                }

                break;
            case 4:
                StartCoroutine(SpecialMonster());       // 미대 전용 스페셜 몹 생성 코루틴

                for (int i = 0; i < WaveNum; i++)
                {
                    for (int j = 0; j < WaveEnemiesNum; j++)
                    {
                        if (j < 5)
                        {
                            EnemyRandom = Random.Range(0, 100) % 2 + 6;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee >= 2 && Ranged >= 2)
                        {
                            EnemyRandom = Random.Range(0, 100) % 2 + 6;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee < 2)
                        {
                            if (Melee == 0)
                            {
                                WaveEnemies[j] = Enemies[6];
                                WaveEnemies[j + 1] = Enemies[6];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[6];
                                EnemyRandom = Random.Range(0, 100) % 2 + 6;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }
                        else if (j >= 5 && Ranged < 2)
                        {
                            if (Ranged == 0)
                            {
                                WaveEnemies[j] = Enemies[7];
                                WaveEnemies[j + 1] = Enemies[7];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[7];
                                EnemyRandom = Random.Range(0, 100) % 2 + 6;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }

                        if (EnemyRandom % 2 == 0)
                            Melee++;
                        else
                            Ranged++;
                    }

                    for (int k = 0; k < WaveEnemiesNum; k++)
                    {
                        Enemy = Instantiate(WaveEnemies[k], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                };     // 라운드, 웨이브마다 다른 개체 수

                EnemyRandom = 0;
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // 적 3마리씩 무한 생성
                {
                    for (int i = 0; i < 3; i++)          // 적 1마리씩 1초마다 소환, 3번 반복
                    {
                        EnemyRandom = Random.Range(0, 100) % 2 + 6;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                }

                break;
            case 5:
                StartCoroutine(SpecialMonster());       // 스페셜몹 소환 코루틴
                

                for (int i = 0; i < WaveNum; i++)
                {
                    WaveEnemyRandom = Random.Range(0, 100) % 4;     // 난수를 4로 나눈 나머지에
                    WaveEnemyRandom *= 2;       // 근거리, 원거리 두 개가 1세트이므로, 2를 곱해준다. 그러면 랜덤 학관의 값이 나옴

                    WaveMelee = WaveEnemyRandom;        // 그러면 근거리는 그 WaveEnemyRandom값 그 자체이고
                    WaveRanged = WaveEnemyRandom + 1;       // 원거리는 +1을 하면 나온다.

                    for (int j = 0; j < WaveEnemiesNum; j++)
                    {
                        if (j < 5)
                        {
                            EnemyRandom = Random.Range(0, 100) % 2 + WaveEnemyRandom;   // 난수를 2로 나눈 나머지에 랜덤학관의 값을 더해줌
                            WaveEnemies[j] = Enemies[EnemyRandom];      // Enemies의 EnemyRandom 인덱스의 오브젝트를 할당함
                        }
                        else if (j >= 5 && Melee >= 2 && Ranged >= 2)
                        {
                            EnemyRandom = Random.Range(0, 100) % 2 + WaveEnemyRandom;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee < 2)
                        {
                            if (Melee == 0)
                            {
                                WaveEnemies[j] = Enemies[WaveMelee];
                                WaveEnemies[j + 1] = Enemies[WaveMelee];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[WaveMelee];
                                EnemyRandom = Random.Range(0, 100) % 2 + WaveEnemyRandom;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }
                        else if (j >= 5 && Ranged < 2)
                        {
                            if (Ranged == 0)
                            {
                                WaveEnemies[j] = Enemies[WaveRanged];
                                WaveEnemies[j + 1] = Enemies[WaveRanged];
                            }
                            else
                            {
                                WaveEnemies[j] = Enemies[WaveRanged];
                                EnemyRandom = Random.Range(0, 100) % 2 + WaveEnemyRandom;
                                WaveEnemies[j + 1] = Enemies[EnemyRandom];
                            }
                            break;
                        }

                        if (EnemyRandom % 2 == 0)
                            Melee++;
                        else
                            Ranged++;
                    }

                    for (int k = 0; k < WaveEnemiesNum; k++)
                    {
                        Enemy = Instantiate(WaveEnemies[k], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                };     // 라운드, 웨이브마다 다른 개체 수

                EnemyRandom = 0;
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // 포탈 체력 0이 될 때까지 적 3마리씩 무한 생성
                {
                    WaveEnemyRandom = Random.Range(0, 100) % 4;         // 랜덤값을 4로 나눈 나머지에
                    WaveEnemyRandom *= 2;       // 근거리, 원거리 두 개가 1세트이므로, 2를 곱해준다

                    for (int i = 0; i < 3; i++)          // 적 1마리씩 1초마다 소환, 3번 반복
                    {
                        EnemyRandom = Random.Range(0, 100) % 2 + WaveEnemyRandom;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1초마다 몬스터 소환
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20초 기다림
                }

                break;
        }
    }

    IEnumerator SpecialMonster()        // 스페셜 몹 생성 코루틴
    {
        while (portalHPControl.e_HP > 0)        // 혹시 모를 상황에 대비한 조건 (이미 포탈 피가 0이하가 되면 모든 코루틴 종료하는 함수가 위에 있음)
        {
            SpEnemyRandom = Random.Range(5, WaveTerm_Seconds);      // 5초~20초 사이 랜덤 초

            yield return new WaitForSeconds(SpEnemyRandom);     // 위에서 뽑은 난수만큼 기다림

            SpEnemy = Instantiate(SpecialMob, GameObject.Find("Monsters").transform);       // 스페셜 몹 생성
            SpEnemy.SetActive(true);
        }
    }

    public void GoldSwitch(bool Switch)     // 골드 생성 시작/정지
    {
        if (Switch == true)
        {
            GoldScript.Instance.InvokeRepeating("GoldUpdate", 1, 0.06667f);
        }
        else
        {
            GoldScript.Instance.CancelInvoke("GoldUpdate");
        }
    }

    public void FinishBtnFunc()     // 게임 클리어 이후 메인화면으로
    {
        SceneManager.LoadScene(TitleScene);
    }



    public void CloseHelpUIFunc()
    {
        HelpUI_Bool = Off;
        Time.timeScale = SpeedBtnValue;       
        HelpUI.SetActive(false);
    }

    public void OpenHelpUIFunc()
    {
        HelpUI_Bool = On;
        Time.timeScale = 0;
        HelpUI.SetActive(true);
    }

    public void SpeedUpFunc()
    {
        switch (SpeedBtnValue)
        {
            case 1f:            // SpeedBtnValue가 1일 때 버튼이 눌리면 1.5배로 올리고, SpeedBtnValue값을 2로 바꿔줌
                Time.timeScale = 1.5f;
                SpeedUpText.text = "x1.5";
                SpeedBtnValue = 1.5f;
                break;
            case 1.5f:
                Time.timeScale = 2f;
                SpeedUpText.text = "x2";
                SpeedBtnValue = 2f;
                break;
            case 2f:
                Time.timeScale = 1f;
                SpeedUpText.text = "x1";
                SpeedBtnValue = 1f;
                break;
        }
    }
}