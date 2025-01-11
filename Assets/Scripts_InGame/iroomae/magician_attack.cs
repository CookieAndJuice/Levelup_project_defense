using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magician_attack : MonoBehaviour
{
    
    public List<GameObject> mobs = new List<GameObject>();

    public List<GameObject> iroomae = new List<GameObject> ();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� enemy �±��� ��쿡�� ó��
        if (collision.CompareTag("C_enemy") || collision.CompareTag("F_enemy") || collision.CompareTag("enemy"))
        {
            // �̹� ��ǥ�� ���ԵǾ� �ִ��� Ȯ��
            if (!mobs.Contains(collision.gameObject))
            {
                mobs.Add(collision.gameObject); // ���ο� �� �߰�
            }
        }
        //����ȸ� ����, ������ ����
        else if(collision.CompareTag("iroomae_tanker") || collision.CompareTag("iroomae_swordman")
            || collision.CompareTag("iroomae_gunner") || collision.CompareTag("iroomae_magician")
            ||collision.CompareTag("iroomae"))
        {
            iroomae.Add(collision.gameObject);
        }
    }

}
