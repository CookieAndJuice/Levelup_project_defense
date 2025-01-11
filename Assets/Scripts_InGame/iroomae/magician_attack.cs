using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magician_attack : MonoBehaviour
{
    
    public List<GameObject> mobs = new List<GameObject>();

    public List<GameObject> iroomae = new List<GameObject> ();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 enemy 태그인 경우에만 처리
        if (collision.CompareTag("C_enemy") || collision.CompareTag("F_enemy") || collision.CompareTag("enemy"))
        {
            // 이미 목표에 포함되어 있는지 확인
            if (!mobs.Contains(collision.gameObject))
            {
                mobs.Add(collision.gameObject); // 새로운 적 추가
            }
        }
        //스페셜몹 공격, 보스몹 공격
        else if(collision.CompareTag("iroomae_tanker") || collision.CompareTag("iroomae_swordman")
            || collision.CompareTag("iroomae_gunner") || collision.CompareTag("iroomae_magician")
            ||collision.CompareTag("iroomae"))
        {
            iroomae.Add(collision.gameObject);
        }
    }

}
