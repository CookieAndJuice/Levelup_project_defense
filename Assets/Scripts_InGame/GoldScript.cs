using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour
{
    public static int Gold;
    public static Text GoldText;

    private static GoldScript instance = null;        // �̱��� ���� �� �� ��ȯ �� ������Ʈ ���� ��ɸ� ����

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(transform.root.gameObject);
        }
    }

    public static GoldScript Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }           // ������� �̱��� ����

    void Start()
    {
        Gold = 100;     // ��差 �ʱ�ȭ
        GoldText = GameObject.Find("HowMuchText").GetComponent<Text>();
    }

    void Update()
    {
        GoldText.text = Gold.ToString();    // ��差�� ��� �ؽ�Ʈ ����ȭ
    }

    public void GoldUpdate()       // ��� �ڵ� �����Լ�
    {
        Gold += 1;
        GoldText.text = Gold.ToString();    // ��差�� ��� �ؽ�Ʈ ����ȭ
    }
}