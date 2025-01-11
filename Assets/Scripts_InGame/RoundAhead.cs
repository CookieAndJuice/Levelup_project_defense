using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;


// �� ��Ż�� �ʸ��� �˾Ƽ� �ǰ� ���̵��� ����� ����.
// 

public class RoundAhead : MonoBehaviour
{
    //public static int EnemyPortalHP = 100;        // ��Ż ���� hp
    //public static int EnemyPortalMaxHP = 100;        // ��Ż max hp
    public static int RoundNum;     // ���� ��
    public string[] RoundName = new string[5];
    public static float StartTimer = 3f;      // ���� Ÿ�̸�
    public string TitleScene;       // ���θ޴� ��

    public static bool On = true;
    public static bool Off = false;

    public Transform MyCharacters;      // �Ʊ� ĳ���͵� ���� ������Ʈ
    public Transform Monsters;          // ���� ĳ���͵� ���� ������Ʈ
    public GameObject Portal;
    public GameObject StartUI;        // ���� ���� �� ������ UI
    public GameObject FinishUI;       // ���� ���� �� ������ UI

    public GameObject FinishBtn;      // ���� Ŭ���� �� ����ȭ������ ���� ��ư

    public Text RoundText;      // ���� �� ���� �ؽ�Ʈ
    public Text RoundStartText;         // ���� ���� �� ������ �ؽ�Ʈ
    public Text RoundFinishText;        // ���� ���� �� ������ �ؽ�Ʈ

    // ���� �� ������ ���� ������
    public static float WaveTerm_Seconds = 20f;      // ���̺� ���� 20��
    public static int WaveNum = 3;          // ���� �� ���̺� �� 3��
    public static int WaveEnemiesNum = 7;       // 1���̺� �� ���� �� 7������

    // �� ������Ʈ ���� ���� ������ �迭
    public GameObject[] Enemies = new GameObject[8];
    // index - 0,1 : 1���� / index - 2,3 : 2���� / ... �̷� ��
    // index - ¦�� : ���� / Ȧ�� : ���Ÿ�

    public GameObject SpecialMob;       // �̴� ����� ����
    public GameObject MiddleBoss;       // �߰� ����
    public GameObject FinalBoss;        // ���� ����

    public static GameObject Enemy;        // �� ������Ʈ ���� ����
    public static GameObject SpEnemy;       // ����� �� ������Ʈ ���� ���� -> ��ȿ�����̱� ��
    public static GameObject[] WaveEnemies = new GameObject[7];        // �� ���̺꿡 ���� ���� ������� ������ ���� �迭

    public static int EnemyRandom = 0;        // �� ���� ���� ���� ���� ���� ����
    public static int Melee = 0;          // 1���̺� �� ���� ������ �� ��
    public static int Ranged = 0;         // 1���̺� �� ���Ÿ� ������ �� ��
    public static int WaveEnemyRandom = 0;      // ���̺긶�� Ư�� ������ ���鳢�� �����ϱ� ���� �־����� ����
    public static int WaveMelee = 0;        // Enemies�迭�� ����� ���� ���� ����
    public static int WaveRanged = 1;       // Enemies�迭�� ����� ���Ÿ� ���� ����
    public static float SpEnemyRandom = 0;        // ����� �� ������ �ֱ�(����)

    public static bool isinteractable = false;

    // ��Ż ü�� ����
    VariablesManager variables;
    public GameObject portal;
    public GameObject boss;
    public enemy portalHPControl;
    public enemy bossHP;



    //���� ����
    public bool HelpUI_isFirst = true;
    public GameObject HelpUI;           // ���� ���� �� ������ ���� UI ---------------
    public static bool HelpUI_Bool = true;       // HelpUI�� ������ �ϴ��� ��Ÿ���� ���� ---------------

    public Button HelpUI_CloseBtn;       // HelpUI â �ݱ� ��ư ---------------
    public Button HelpUI_OpenBtn;           // HelpUI â ���� ��ư ---------------

    bool iscorutinestopped = false;

    // ������ ���� ����
    public string[] LastMssgStartArray = new string[4];         // ������ ���� ���� �� �ؽ�Ʈ
    public string[] LastMssgFinishArray = new string[3];        // ������ ���� ���� �� �ؽ�Ʈ
    public int CharPerSeconds;          // �ؽ�Ʈ ��� �ӵ�

    public GameObject LastRoundMssgBox;      // ������ ���� �ؽ�Ʈ UI
    public GameObject Iroomae;     // �̷�� �ʻ�ȭ
    public GameObject Boss;        // ������ �ʻ�ȭ
    public GameObject EnterCursor;          // ��ȭ ��ġ Ŀ��

    public Text LastRoundMssgText;           // ������ ���� �ؽ�Ʈ
    public Sprite DownedBoss;
    public Image BossImage;



    // ��� ����
    public static float SpeedBtnValue = 1f;      // ���� �������� ���¸� ��Ÿ�� �� | 1 : 1���, 1.5 : 1.5���, 2 : 2���

    public static Button SpeedUpBtn;           // �������� ��ư

    public Text SpeedUpText;        // �������� ��ư�� �ؽ�Ʈ


    //�̵� ���� ����
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
        FinishBtn.GetComponent<Button>().onClick.AddListener(FinishBtnFunc);        // ���θ޴� ���� �Լ� ��ư�� �Ҵ�

        // ���� �ؽ�Ʈ ���� �ʱ�ȭ
        RoundText.text = RoundName[RoundNum-1];

        // gamble ��ư, �Ͻ����� ��ư ��Ȱ��ȭ
        RoundAhead.isinteractable = false;
        Click_Function.GambleBtn.interactable = false;
        PauseScript.PauseBtn.interactable = false;
        HelpUI_OpenBtn.interactable = false;

        // ���� ���� â
        HelpUI_CloseBtn.onClick.AddListener(CloseHelpUIFunc);
        HelpUI_OpenBtn.onClick.AddListener(OpenHelpUIFunc);
        OpenHelpUIFunc();

        SpeedUpBtn = GameObject.Find("SpeedUpBtn").GetComponent<Button>();          // static ������ ���� �Ҵ��� ��

        SpeedUpBtn.onClick.AddListener(SpeedUpFunc);        // �������� ��ư�� �Լ� �Ҵ�

        SpeedUpBtn.interactable = false;

        for (int i = 0; i < 4; i++)
        {
            Click_Function.CharacterBtn[i].interactable = false;
        }

        SpeedBtnValue = 1f;

        // ���� ���۱��� 3�� ����
        //StartUI.SetActive(true);
        //RoundStartText.text = "Round " + RoundNum;

        //StartCoroutine(RoundStart(StartTimer));// ���� ���� �ڷ�ƾ

    }

    void Update()
    {

        if (portalHPControl.e_HP <= 0 && RoundNum <= 4)
        {
            
            // ���� �� ����. �� �� �Լ����� RoundNum�� 2, 3, 4, 5�� �� ����.
            RoundNum += 1;
            // ��Ż ü�� ����
            portalHPControl.max_HP = variables.portal_HP[RoundNum - 1];
            portalHPControl.e_HP = portalHPControl.max_HP;

            SpeedUpBtn.interactable = false;
            // ������� ���� ��Ż ���߰��� �ִϸ��̼� ������ ������
            Portal.SetActive(false);        // ��Ż �����

            // ��� ĳ���͵� ����. ���� X, �̵� X, ĳ���� ���� X, �Ͻ����� X
            /// ¥�ߵ� ------------------------------------------------------------------------------
            /// 

            isinteractable = false;
            Click_Function.GambleBtn.interactable = false;
            PauseScript.PauseBtn.interactable = false;
            HelpUI_OpenBtn.interactable = false;

            FinishUI.SetActive(true);       // finishUI ����

            StopAllCoroutines();
            CancelInvoke("EnemyPortalFunc");
            GoldSwitch(Off);
            Invoke("NextRoundFunc", 5 * SpeedBtnValue);      // 5�� �� ���� ���� ����
        }
        else if (RoundNum == 1 && HelpUI_isFirst == true && HelpUI_Bool == Off)       // 1�����̰�, ���� ���� �ʱ��̰�, HelpUI�� �����ٸ�
        {
            HelpUI_isFirst = false;

            StartUI.SetActive(true);
            RoundStartText.text = RoundName[RoundNum - 1];

            StartCoroutine(RoundStart(StartTimer));         // ���� ���� �ڷ�ƾ
        }
        else if(bossHP.e_HP <= 0 && RoundNum == 5)
        {
            foreach (Transform child in MyCharacters)   // ĳ����, �� ������Ʈ ����
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
        FinishUI.SetActive(false);      // finishUI ����

        if (RoundNum <= 4)       // �� 6��������� ����
        {

            foreach (Transform child in MyCharacters)   // ĳ����, �� ������Ʈ ����
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in Monsters)
            {
                Destroy(child.gameObject);
            }


            // ���� ���۱��� 3�� ����
            StartUI.SetActive(true);
            RoundStartText.text = RoundName[RoundNum - 1];
            StartCoroutine(RoundStart(StartTimer));     // ���� ���� �ڷ�ƾ

            CancelInvoke("NextRoundFunc");
        }
        else if(RoundNum == 5)
        {
            StartCoroutine(LastRoundRoutine());
        }
    }

    IEnumerator LastRoundRoutine()           // 5���� �ʱ�ȭ �ڷ�ƾ
    {
        CancelInvoke("NextRoundFunc");


        foreach (Transform child in MyCharacters)   // ĳ����, �� ������Ʈ ����
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

        // �ؽ�Ʈ ����Ʈ ����(�̷�� - ������ ��ȭ ���)
        StringBuilder msgSB = new StringBuilder();          // stringbuilder ����
        LastRoundMssgText.text = "";          // ��ȭ �ؽ�Ʈ �ʱ�ȭ
        LastRoundMssgBox.SetActive(true);           // ��ȭ ���� ON
        Iroomae.SetActive(true);                // �̷�� ��ȭ ����
        for (int i = 0; i < 4; i++)
        {
            EnterCursor.SetActive(false);
            msgSB.Clear();          // stringbuilder �ʱ�ȭ
            LastRoundMssgText.text = "";          // ��ȭ �ؽ�Ʈ �ʱ�ȭ
            if (i == 1)
            {
                Iroomae.SetActive(false);                                       // �̷�� ��ȭ ����
                LastRoundMssgText.alignment = TextAnchor.MiddleRight;           // ������ ����
                Boss.SetActive(true);                                           // ������ ��ȭ ����
            }

            for (int j = 0; j < LastMssgStartArray[i].Length; j++)          // ���� Ÿ���� ����Ʈ
            {
                msgSB.Append(LastMssgStartArray[i][j]);         // stringbuilder�� ���� �ϳ��� �߰�
                LastRoundMssgText.text = msgSB.ToString();
                yield return new WaitForSecondsRealtime(1.0f / CharPerSeconds);
            }

            EnterCursor.SetActive(true);            // ��ȭâ ���� ���

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));          // ȭ�� ��ġ�ؾ� �Ѿ
        }
        //Time.timeScale = SpeedBtnValue;
        LastRoundMssgBox.SetActive(false);
        RoundStartText.text = "Final Round";
        StartUI.SetActive(true);

        StartCoroutine(RoundStart(StartTimer));     // ���� ���� �ڷ�ƾ

        StopCoroutine(LastRoundRoutine());
    }

    IEnumerator FinishRoutine()         // ���� ���� �ڷ�ƾ
    {
        // �ʵ� ���� ���� �ʱ�ȭ
        foreach (Transform child in Monsters)       // �� ������Ʈ ����
        {
            Destroy(child.gameObject);
        }

        // �̹����� ������ ������ �̹����� ��ȯ
        BossImage.sprite = DownedBoss;

        // �ؽ�Ʈ ����Ʈ ����(������ ���� ���� ���)
        StringBuilder msgSB = new StringBuilder();
        LastRoundMssgText.text = string.Empty;          // ��ȭ �ؽ�Ʈ �ʱ�ȭ
        LastRoundMssgBox.SetActive(true);           // ��ȭ ���� ON
        for (int i = 0; i < 3; i++)
        {
            EnterCursor.SetActive(false);
            msgSB.Clear();                                  // stringbuilder �ʱ�ȭ
            LastRoundMssgText.text = string.Empty;          // ��ȭ �ؽ�Ʈ �ʱ�ȭ

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




        RoundFinishText.text = "�����մϴ�!!";
        FinishUI.SetActive(true);
        FinishBtn.SetActive(true);
    }

    IEnumerator RoundStart(float TimerN)            // ���� ���� �ڷ�ƾ
    {
        RoundStartText.text = RoundName[RoundNum - 1];
        yield return new WaitForSecondsRealtime(TimerN);        // TimerN��ŭ(3��) ��ٸ�
        RoundStartText.text = "Start!";     // ��� �غ� -> ��� ����! ���� �ؽ�Ʈ ����
        
        SpeedUpBtn.interactable = true;
        // 1�� �� StartUI.SetActive(false), 
        yield return new WaitForSecondsRealtime(1.0f);
        StartUI.SetActive(false);

        if (RoundNum == 5)
            Time.timeScale = SpeedBtnValue;

        // ���� ���� : ����, �̵�, ĳ���� ����, �Ͻ����� ��� ���󺹱�
        //PlayerMobScript.UnitStop = false;
        //EnemyScript.UnitStop = false;                                       ¥�ߵ�-----------------------------------------------------------------

        isinteractable = true;
        Click_Function.GambleBtn.interactable = true;
        PauseScript.PauseBtn.interactable = true;
        HelpUI_OpenBtn.interactable = true;
        for (int i = 0; i < 4; i++)
        {
            Click_Function.CharacterBtn[i].interactable = true;
        }

        Portal.SetActive(true);     // ��Ż ����
        GoldSwitch(On);     // ��� ���� ����

        // �� ���� �ڵ�
        // Enemies �迭�� ����� �ִ� �� ������Ʈ�� ��, ���忡 �°� �������� WaveEnemies �迭�� ������� ������.
        // �� ���̺긶�� WaveEnemies �迭�� ����, ���, �ʱ�ȭ -> �� 3�ܰ踦 ��ħ.
        switch (RoundNum)       // ���忡 ���� ������ �ٸ��� �����ǵ��� ��
        {
            case 1:         // ���� 1
                for (int i = 0; i < WaveNum; i++)           // ���̺� 3�� ���� �ݺ�
                {
                    for (int j = 0; j < WaveEnemiesNum; j++)        // 1���̺꿡 7������ ������, �������� ����2, ���Ÿ�2 �̻� ������ �Ҵ�
                    {
                        if (j < 5)          // 5�������� �������� ����
                        {
                            EnemyRandom = Random.Range(0, 100) % 2;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee >= 2 && Ranged >= 2)       // 6, 7��°�� ��2 ��2 ������ ���߱� ���� if������ �ذ�
                        {
                            EnemyRandom = Random.Range(0, 100) % 2;
                            WaveEnemies[j] = Enemies[EnemyRandom];
                        }
                        else if (j >= 5 && Melee < 2)       // 6,7��°���� ���� �ȵ� ��� �ݺ� ���� �� �� �Ҵ��ϰ� �ݺ��� ����
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
                        else if (j >= 5 && Ranged < 2)       // 6,7��°���� ���� �ȵ� ��� �ݺ� ���� �� �� �Ҵ��ϰ� �ݺ��� ����
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

                        if (EnemyRandom % 2 == 0)       // ����, ���Ÿ� ���� ��
                            Melee++;
                        else
                            Ranged++;
                    }

                    for (int k = 0; k < WaveEnemiesNum; k++)        // WaveEnemies �迭�� ������� ����� �� ������Ʈ�� ���� �ڵ�
                    {
                        Enemy = Instantiate(WaveEnemies[k], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
                };     // ����, ���̺긶�� �ٸ� ��ü ��

                EnemyRandom = 0;        // ���� ���忡�� ��Ȱ���ϱ� ���� ���� �ʱ�ȭ
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // �� 3������ ���� ����
                {
                    for (int i = 0; i < 3; i++)          // �� 1������ 1�ʸ��� ��ȯ, 3�� �ݺ�
                    {
                        EnemyRandom = Random.Range(0, 100) % 2;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
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

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
                };     // ����, ���̺긶�� �ٸ� ��ü ��

                EnemyRandom = 0;
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // �� 3������ ���� ����
                {
                    for (int i = 0; i < 3; i++)          // �� 1������ 1�ʸ��� ��ȯ, 3�� �ݺ�
                    {
                        EnemyRandom = Random.Range(0, 100) % 2 + 2;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
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

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
                };     // ����, ���̺긶�� �ٸ� ��ü ��

                EnemyRandom = 0;
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // �� 3������ ���� ����
                {
                    for (int i = 0; i < 3; i++)          // �� 1������ 1�ʸ��� ��ȯ, 3�� �ݺ�
                    {
                        EnemyRandom = Random.Range(0, 100) % 2 + 4;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
                }

                break;
            case 4:
                StartCoroutine(SpecialMonster());       // �̴� ���� ����� �� ���� �ڷ�ƾ

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

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
                };     // ����, ���̺긶�� �ٸ� ��ü ��

                EnemyRandom = 0;
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // �� 3������ ���� ����
                {
                    for (int i = 0; i < 3; i++)          // �� 1������ 1�ʸ��� ��ȯ, 3�� �ݺ�
                    {
                        EnemyRandom = Random.Range(0, 100) % 2 + 6;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
                }

                break;
            case 5:
                StartCoroutine(SpecialMonster());       // ����ȸ� ��ȯ �ڷ�ƾ
                

                for (int i = 0; i < WaveNum; i++)
                {
                    WaveEnemyRandom = Random.Range(0, 100) % 4;     // ������ 4�� ���� ��������
                    WaveEnemyRandom *= 2;       // �ٰŸ�, ���Ÿ� �� ���� 1��Ʈ�̹Ƿ�, 2�� �����ش�. �׷��� ���� �а��� ���� ����

                    WaveMelee = WaveEnemyRandom;        // �׷��� �ٰŸ��� �� WaveEnemyRandom�� �� ��ü�̰�
                    WaveRanged = WaveEnemyRandom + 1;       // ���Ÿ��� +1�� �ϸ� ���´�.

                    for (int j = 0; j < WaveEnemiesNum; j++)
                    {
                        if (j < 5)
                        {
                            EnemyRandom = Random.Range(0, 100) % 2 + WaveEnemyRandom;   // ������ 2�� ���� �������� �����а��� ���� ������
                            WaveEnemies[j] = Enemies[EnemyRandom];      // Enemies�� EnemyRandom �ε����� ������Ʈ�� �Ҵ���
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

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
                };     // ����, ���̺긶�� �ٸ� ��ü ��

                EnemyRandom = 0;
                Melee = 0;
                Ranged = 0;

                while (portalHPControl.e_HP >= 0)     // ��Ż ü�� 0�� �� ������ �� 3������ ���� ����
                {
                    WaveEnemyRandom = Random.Range(0, 100) % 4;         // �������� 4�� ���� ��������
                    WaveEnemyRandom *= 2;       // �ٰŸ�, ���Ÿ� �� ���� 1��Ʈ�̹Ƿ�, 2�� �����ش�

                    for (int i = 0; i < 3; i++)          // �� 1������ 1�ʸ��� ��ȯ, 3�� �ݺ�
                    {
                        EnemyRandom = Random.Range(0, 100) % 2 + WaveEnemyRandom;
                        Enemy = Instantiate(Enemies[EnemyRandom], GameObject.Find("Monsters").transform);
                        Enemy.SetActive(true);

                        yield return new WaitForSeconds(1.0f);      // 1�ʸ��� ���� ��ȯ
                    }
                    EnemyRandom = 0;

                    yield return new WaitForSeconds(WaveTerm_Seconds);     // 20�� ��ٸ�
                }

                break;
        }
    }

    IEnumerator SpecialMonster()        // ����� �� ���� �ڷ�ƾ
    {
        while (portalHPControl.e_HP > 0)        // Ȥ�� �� ��Ȳ�� ����� ���� (�̹� ��Ż �ǰ� 0���ϰ� �Ǹ� ��� �ڷ�ƾ �����ϴ� �Լ��� ���� ����)
        {
            SpEnemyRandom = Random.Range(5, WaveTerm_Seconds);      // 5��~20�� ���� ���� ��

            yield return new WaitForSeconds(SpEnemyRandom);     // ������ ���� ������ŭ ��ٸ�

            SpEnemy = Instantiate(SpecialMob, GameObject.Find("Monsters").transform);       // ����� �� ����
            SpEnemy.SetActive(true);
        }
    }

    public void GoldSwitch(bool Switch)     // ��� ���� ����/����
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

    public void FinishBtnFunc()     // ���� Ŭ���� ���� ����ȭ������
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
            case 1f:            // SpeedBtnValue�� 1�� �� ��ư�� ������ 1.5��� �ø���, SpeedBtnValue���� 2�� �ٲ���
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