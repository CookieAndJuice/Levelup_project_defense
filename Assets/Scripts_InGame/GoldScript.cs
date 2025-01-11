using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour
{
    public static int Gold;
    public static Text GoldText;

    private static GoldScript instance = null;        // 싱글톤 패턴 중 씬 전환 시 오브젝트 유지 기능만 삭제

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
    }           // 여기까지 싱글톤 패턴

    void Start()
    {
        Gold = 100;     // 골드량 초기화
        GoldText = GameObject.Find("HowMuchText").GetComponent<Text>();
    }

    void Update()
    {
        GoldText.text = Gold.ToString();    // 골드량과 골드 텍스트 동기화
    }

    public void GoldUpdate()       // 골드 자동 생성함수
    {
        Gold += 1;
        GoldText.text = Gold.ToString();    // 골드량과 골드 텍스트 동기화
    }
}