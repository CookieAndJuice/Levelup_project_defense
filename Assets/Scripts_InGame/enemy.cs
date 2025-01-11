using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    // 적 변수 모음
    VariablesManager variables;
    public int enemy_speed;
    bool onattack = false;
    public float e_HP;
    public float e_att;
    public float e_attvelo;
    public float e_dis;
    static int enemy_order = 2;
    SpriteRenderer spriteRenderer;



    //애니메이션 변수
    public Animator anim;

    // 이펙트 오브젝트 모음
    public GameObject effect;
    public GameObject effect2;
    public GameObject effect3;
    public GameObject effect4;

    public GameObject boss_C_explosion_effect;
    public GameObject boss_F_smoke_effect;


    public AnimationCurve curve2;

    //죽음 이펙트 관련
    public AnimationCurve curve;

    public bool isdied = false;



    public Transform portal;
    public Slider hpbar;
    public float max_HP;


    
    void Awake()
    {
        GameObject VM = GameObject.Find("Variables_Manager");
        variables = VM.GetComponent<VariablesManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = enemy_order;
        enemy_order += 1;

    }
    void Start()
    {
        if(gameObject.name == "enemy_01(Clone)" || gameObject.name == "enemy_01")
        {
            e_HP = variables.enemy_01[0];
            e_att = variables.enemy_01[1];
            e_attvelo = variables.enemy_01[2];
            e_dis = variables.enemy_01[3];
        }
        else if(gameObject.name == "enemy_02(Clone)" || gameObject.name == "enemy_02")
        {
            e_HP = variables.enemy_02[0];
            e_att = variables.enemy_02[1];
            e_attvelo = variables.enemy_02[2];
            e_dis = variables.enemy_02[3];
        }
        else if (gameObject.name == "enemy_03(Clone)" || gameObject.name == "enemy_03")
        {
            //보이지 않는 손 투명도 조절
            transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            e_HP = variables.enemy_03[0];
            e_att = variables.enemy_03[1];
            e_attvelo = variables.enemy_03[2];
            e_dis = variables.enemy_03[3];
        }
        else if (gameObject.name == "enemy_04(Clone)" || gameObject.name == "enemy_04")
        {
            e_HP = variables.enemy_04[0];
            e_att = variables.enemy_04[1];
            e_attvelo = variables.enemy_04[2];
            e_dis = variables.enemy_04[3];
        }
        else if (gameObject.name == "enemy_05(Clone)" || gameObject.name == "enemy_05")
        {
            e_HP = variables.enemy_05[0];
            e_att = variables.enemy_05[1];
            e_attvelo = variables.enemy_05[2];
            e_dis = variables.enemy_05[3];
        }
        else if (gameObject.name == "enemy_06(Clone)" || gameObject.name == "enemy_06")
        {
            e_HP = variables.enemy_06[0];
            e_att = variables.enemy_06[1];
            e_attvelo = variables.enemy_06[2];
            e_dis = variables.enemy_06[3];
        }
        else if (gameObject.name == "enemy_07(Clone)" || gameObject.name == "enemy_07")
        {
            e_HP = variables.enemy_07[0];
            e_att = variables.enemy_07[1];
            e_attvelo = variables.enemy_07[2];
            e_dis = variables.enemy_07[3];
        }
        else if (gameObject.name == "enemy_08(Clone)" || gameObject.name == "enemy_08")
        {
            e_HP = variables.enemy_08[0];
            e_att = variables.enemy_08[1];
            e_attvelo = variables.enemy_08[2];
            e_dis = variables.enemy_08[3];
        }
        else if (gameObject.name == "enemy_special(Clone)" || gameObject.name == "enemy_special")
        {
            e_HP = variables.enemy_special[0];
            e_att = variables.enemy_special[1];
            e_attvelo = variables.enemy_special[2];
            e_dis = variables.enemy_special[3];
            enemy_speed = 2;
        }
        else if (gameObject.name == "enemy_boss(Clone)" || gameObject.name == "enemy_boss")
        {
            e_HP = variables.enemy_boss[0];
            e_att = variables.enemy_boss[1];
            e_attvelo = variables.enemy_boss[2];
            e_dis = variables.enemy_boss[3];
        }
        else
        {
            e_HP = variables.portal_HP[0];
        }

        if (RoundAhead.RoundNum == 5)
        {
            if (CompareTag("C_enemy"))
            {
                e_HP = variables.finalRound_C[0];
                e_att = variables.finalRound_C[1];
                e_attvelo = variables.finalRound_C[2];
                e_dis = variables.finalRound_C[3];
            }
            else if (CompareTag("F_enemy"))
            {
                e_HP = variables.finalRound_F[0];
                e_att = variables.finalRound_F[1];
                e_attvelo = variables.finalRound_F[2];
                e_dis = variables.finalRound_F[3];
            }
        }
        
        max_HP = e_HP;

    }

    void Update()
    {
        
        if(name != "portal")
        {
            if(isdied == false)
            {
                if(hpbar != null)
                {
                    hpbar.value = e_HP / max_HP;
                }
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.left, e_dis,
                LayerMask.GetMask("iroomae"));
                Debug.DrawRay(transform.position, Vector2.left * e_dis, Color.red);

                if (hitInfo.collider != null)
                {
                    transform.position = transform.position;
                    if (onattack == false)
                    {
                        
                        
                        onattack = true;
                        if(isdied == false)
                            StartCoroutine(Deal(hitInfo.collider.GetComponent<iroomae_subdivide>(), e_att, e_attvelo));
                        else
                            StopCoroutine(Deal(hitInfo.collider.GetComponent<iroomae_subdivide>(), e_att, e_attvelo));



                    }
                }
                else
                {
                    if (onattack == true)
                    {
                        transform.position = transform.position;
                    }
                    else
                    {
                        if(gameObject.name == "enemy_03" || gameObject.name == "enemy_03(Clone)")
                            transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
                        if (anim != null)
                            anim.SetInteger("state", 0);

                        
                        transform.position = transform.position + Vector3.left * enemy_speed * Time.deltaTime;
                        
                    }
                    
                    
                }
            }
            else
            {
                if(name == "enemy_boss" && RoundAhead.RoundNum == 6 && anim.GetInteger("state") == 3)
                {
                    transform.position = transform.position + Vector3.right * enemy_speed * Time.deltaTime;
                }
                else
                {
                    transform.position = transform.position;
                }
            }
        }
        else
        {
            hpbar.value = e_HP / max_HP;
        }

    }

    IEnumerator Deal(iroomae_subdivide iroomae_Subdivide, float damage, float attvelo)
    {
        if(gameObject.name == "enemy_01(Clone)" || gameObject.name == "enemy_01"
            || gameObject.name == "enemy_05(Clone)" || gameObject.name == "enemy_05"
            || gameObject.name == "enemy_07(Clone)" || gameObject.name == "enemy_07")
        {
            anim.SetInteger("state", 1);
            yield return new WaitForSeconds(attvelo * 0.1f);
            // 이루매 데미지 입히기
            if (iroomae_Subdivide != null)
            {
                iroomae_Subdivide.GetComponent<iroomae_subdivide>().Damaged(damage);
            }
            anim.SetInteger("state", 2);
            yield return new WaitForSeconds(attvelo * 0.9f);
        }
        else if(gameObject.name == "enemy_02(Clone)" || gameObject.name == "enemy_02")
        {
            anim.SetInteger("state", 1);
            GameObject picked_effect = null;
            int rand_effect = Random.Range(0, 4);
            switch (rand_effect)
            {
                case 0:
                    picked_effect = effect;
                    break;
                case 1:
                    picked_effect = effect2;
                    break;
                case 2:
                    picked_effect = effect3;
                    break;
                case 3:
                    picked_effect = effect4;
                    break;
            }
            picked_effect.transform.position = transform.position;
            if (isdied == false)
                picked_effect.SetActive(true);

            float duration = 0.5f;
            float time = 0.0f;
            Vector3 start = transform.position;
            while (time < duration)
            {
                time += Time.deltaTime;
                float linearT = time / duration;
                float heightT = curve2.Evaluate(linearT);

                float height = Mathf.Lerp(0.0f, 1, heightT);

                picked_effect.transform.position = Vector2.Lerp(start, 
                    iroomae_Subdivide.transform.position, linearT) + new Vector2(0.0f, height);

                if (iroomae_Subdivide.GetComponent<iroomae_subdivide>().isdied == true)
                {
                    if (effect != null)
                        effect.SetActive(false);
                    if (effect2 != null)
                        effect2.SetActive(false);
                    if (effect3 != null)
                        effect3.SetActive(false);
                    if (effect4 != null)
                        effect4.SetActive(false);
                    onattack = false;
                    yield break;
                }

                yield return null;
            }
            // 이루매 데미지 입히기
            if (iroomae_Subdivide != null && isdied != true)
            {
                iroomae_Subdivide.GetComponent<iroomae_subdivide>().Damaged(damage);
            }
            picked_effect.SetActive(false);
            anim.SetInteger("state", 2);
            yield return new WaitForSeconds(attvelo);
        }
        else if (gameObject.name == "enemy_03(Clone)" || gameObject.name == "enemy_03")
        {
            transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            anim.SetInteger("state", 1);
            yield return new WaitForSeconds(attvelo * 0.1f);
            // 이루매 데미지 입히기
            if (iroomae_Subdivide != null)
            {
                iroomae_Subdivide.GetComponent<iroomae_subdivide>().Damaged(damage);
            }
            anim.SetInteger("state", 2);
            yield return new WaitForSeconds(attvelo * 0.9f);
        }
        else if (gameObject.name == "enemy_04(Clone)" || gameObject.name == "enemy_04")
        {
            anim.SetInteger("state", 1);
            GameObject picked_effect = null;
            int rand_effect = Random.Range(0, 4);
            switch (rand_effect)
            {
                case 0:
                    picked_effect = effect;
                    break;
                case 1:
                    picked_effect = effect2;
                    break;
                case 2:
                    picked_effect = effect3;
                    break;
                case 3:
                    picked_effect = effect4;
                    break;
            }
            picked_effect.transform.position = transform.position;
            if (isdied == false)
                picked_effect.SetActive(true);

            float duration = 0.5f;
            float time = 0.0f;
            Vector3 start = transform.position;
            while (time < duration)
            {
                time += Time.deltaTime;
                float linearT = time / duration;
                float heightT = curve2.Evaluate(linearT);

                float height = Mathf.Lerp(0.0f, 1, heightT);

                picked_effect.transform.position = Vector2.Lerp(start,
                    iroomae_Subdivide.transform.position, linearT) + new Vector2(0.0f, height);

                if (iroomae_Subdivide.GetComponent<iroomae_subdivide>().isdied == true)
                {
                    if (effect != null)
                        effect.SetActive(false);
                    if (effect2 != null)
                        effect2.SetActive(false);
                    if (effect3 != null)
                        effect3.SetActive(false);
                    if (effect4 != null)
                        effect4.SetActive(false);
                    onattack = false;
                    yield break;
                }

                yield return null;
            }
            // 이루매 데미지 입히기
            if (iroomae_Subdivide != null && isdied != true)
            {
                iroomae_Subdivide.GetComponent<iroomae_subdivide>().Damaged(damage);
            }
            picked_effect.SetActive(false);
            anim.SetInteger("state", 2);
            yield return new WaitForSeconds(attvelo);
        }
        else if (gameObject.name == "enemy_08(Clone)" || gameObject.name == "enemy_08")
        {
            anim.SetInteger("state", 1);
            effect.transform.position = transform.position;
            yield return new WaitForSeconds(attvelo * 0.2f);
            if (isdied == false)
                effect.SetActive(true);

            float duration = 0.4f;
            float time = 0.0f;
            Vector3 start = transform.position + Vector3.left + Vector3.up *0.7f;
            while (time < duration)
            {
                time += Time.deltaTime;
                float linearT = time / duration;
                float heightT = curve2.Evaluate(linearT);

                float height = Mathf.Lerp(0.0f, 1, heightT);

                effect.transform.position = Vector2.Lerp(start,
                    iroomae_Subdivide.transform.position, linearT) + new Vector2(0.0f, height);

                if (iroomae_Subdivide.GetComponent<iroomae_subdivide>().isdied == true)
                {
                    if (effect != null)
                        effect.SetActive(false);
                    if (effect2 != null)
                        effect2.SetActive(false);
                    if (effect3 != null)
                        effect3.SetActive(false);
                    if (effect4 != null)
                        effect4.SetActive(false);
                    onattack = false;
                    yield break;
                }

                yield return null;
            }
            // 이루매 데미지 입히기
            effect2.transform.position = iroomae_Subdivide.transform.position + Vector3.left;
            if(isdied == false)
                effect2.SetActive(true);
            if (iroomae_Subdivide != null && isdied != true)
            {
                iroomae_Subdivide.GetComponent<iroomae_subdivide>().Damaged(damage);
            }
            effect.SetActive(false);
            yield return new WaitForSeconds(attvelo * 0.4f);
            effect2.SetActive(false);
            anim.SetInteger("state", 2);
            yield return new WaitForSeconds(attvelo * 0.4f);
        }
        else if (gameObject.name == "enemy_06(Clone)" || gameObject.name == "enemy_06")
        {
            anim.SetInteger("state", 1);
            GameObject picked_effect = null;
            int rand_effect = Random.Range(0, 4);
            switch (rand_effect)
            {
                case 0:
                    picked_effect = effect;
                    break;
                case 1:
                    picked_effect = effect2;
                    break;
                case 2:
                    picked_effect = effect3;
                    break;
                case 3:
                    picked_effect = effect4;
                    break;
            }
            picked_effect.transform.position = transform.position;
            if (isdied == false)
                picked_effect.SetActive(true);

            float duration = 1f;
            float time = 0.0f;
            Vector3 start = transform.position;
            while (time < duration)
            {
                time += Time.deltaTime;
                float linearT = time / duration;
                float heightT = curve2.Evaluate(linearT *2);

                float height = Mathf.Lerp(0, 1, heightT);

                picked_effect.transform.position = Vector2.Lerp(start,
                    iroomae_Subdivide.transform.position, linearT) + new Vector2(0.0f, height);

                if (iroomae_Subdivide.GetComponent<iroomae_subdivide>().isdied == true)
                {
                    if (effect != null)
                        effect.SetActive(false);
                    if (effect2 != null)
                        effect2.SetActive(false);
                    if (effect3 != null)
                        effect3.SetActive(false);
                    if (effect4 != null)
                        effect4.SetActive(false);
                    onattack = false;
                    yield break;
                }
                yield return null;
            }
            // 이루매 데미지 입히기
            if (iroomae_Subdivide != null && isdied != true)
            {
                iroomae_Subdivide.GetComponent<iroomae_subdivide>().Damaged(damage);
            }
            picked_effect.SetActive(false);
            anim.SetInteger("state", 2);
            yield return new WaitForSeconds(attvelo);
        }
        else if(gameObject.name == "enemy_special" || gameObject.name == "enemy_special(Clone)")
        {
            Vector3 explosion_pos = transform.position + Vector3.up*0.8f;
            anim.SetInteger("state", 3);
            yield return new WaitForSeconds(3f);
            anim.SetInteger("state", 1);
            yield return new WaitForSeconds(0.3f);
            GameObject ins = effect;
            if(isdied == false)
            {
                ins = Instantiate(effect, explosion_pos, Quaternion.identity);
                ins.SetActive(true);
            }
            if(isdied == false)
                yield return new WaitForSeconds(0.2f);

            foreach (GameObject ir in ins.GetComponent<magician_attack>().iroomae)
            {
                if(isdied == true)
                {
                    ins.SetActive(false);
                    break;
                }
                if(ir != null)
                    ir.GetComponent<iroomae_subdivide>().Damaged(damage);
            }
            ins.GetComponent<magician_attack>().iroomae.Clear();
            Damaged(10000);
            yield return new WaitForSeconds(0.8f);
            ins.SetActive(false);

        }
        else if(gameObject.name == "enemy_boss" || gameObject.name == "enemy_boss(Clone)")
        {
            anim.SetInteger("state", 1);

            float DamagePercent;

            GameObject picked_effect = null;
            int rand_effect = Random.Range(0, 300);
            if(rand_effect < 45)
            {
                picked_effect = effect4;
                DamagePercent = 1f;
            }
            else if(rand_effect >= 45 && rand_effect < 130)
            {
                picked_effect = effect2;
                DamagePercent = 0.3f;
            }
            else if(rand_effect >= 130 && rand_effect < 215)
            {
                picked_effect = effect3;
                DamagePercent = 0.35f;
            }
            else
            {
                picked_effect = effect;
                DamagePercent = 0.25f;
            }

            picked_effect.transform.position = transform.position;
            if (isdied == false)
                picked_effect.SetActive(true);

            if (picked_effect == effect4)
            {
                picked_effect.transform.position = iroomae_Subdivide.transform.position + Vector3.up * 15;
                // F 떨구기 코드
                float delta = 0;
                float duration = 0.3f;

                while (delta <= duration)
                {
                    float t = delta / duration;
                    picked_effect.transform.position = Vector3.Lerp(iroomae_Subdivide.transform.position + Vector3.up * 20,
                        new Vector3(iroomae_Subdivide.transform.position.x, -3f,iroomae_Subdivide.transform.position.z), t);


                    delta += Time.deltaTime;
                    yield return null;
                }

                anim.SetInteger("state", 2);
                boss_F_smoke_effect.transform.position = picked_effect.transform.position + Vector3.up;
                if(isdied == false)
                    boss_F_smoke_effect.SetActive(true);
                // 이루매 데미지 입히는 코드
                foreach (GameObject ir in effect4.GetComponent<magician_attack>().iroomae)
                {
                    if (isdied == true)
                    {
                        effect4.SetActive(false);
                        break;
                    }
                    

                    if (ir != null)
                    {
                        float ir_maxHP = ir.GetComponent<iroomae_subdivide>().max_HP;
                        ir.GetComponent<iroomae_subdivide>().Damaged(ir_maxHP * DamagePercent);
                    }
                }
                effect4.GetComponent<magician_attack>().iroomae.Clear();

                yield return new WaitForSeconds(attvelo * 0.8f);

                boss_F_smoke_effect.SetActive(false);
                picked_effect.SetActive(false);
                
                yield return new WaitForSeconds(attvelo * 0.5f);
            }
            else
            {
                // C 뿌리기 코드
                float duration = 0.5f;
                float time = 0.0f;
                Vector3 start = transform.position + Vector3.left*2;
                while (time < duration)
                {
                    time += Time.deltaTime;
                    float linearT = time / duration;
                    float heightT = curve2.Evaluate(linearT);

                    float height = Mathf.Lerp(0.0f, 1, heightT);

                    picked_effect.transform.position = Vector2.Lerp(start,
                        transform.position + Vector3.left * 7.5f + Vector3.down*1.2f, linearT) + new Vector2(0.0f, height);

                    if (iroomae_Subdivide.GetComponent<iroomae_subdivide>().isdied == true)
                    {
                        if (effect != null)
                            effect.SetActive(false);
                        if (effect2 != null)
                            effect2.SetActive(false);
                        if (effect3 != null)
                            effect3.SetActive(false);
                        if (effect4 != null)
                            effect4.SetActive(false);
                        onattack = false;
                        yield break;
                    }

                    yield return null;
                }
                anim.SetInteger("state", 2);
                yield return new WaitForSeconds(attvelo * 0.5f);
                boss_C_explosion_effect.transform.position = picked_effect.transform.position + Vector3.up*1.4f;
                if(isdied == false)
                    boss_C_explosion_effect.SetActive(true);

                yield return new WaitForSeconds(0.01f);
                // 이루매 데미지 입히는 코드
                foreach (GameObject ir in boss_C_explosion_effect.GetComponent<magician_attack>().iroomae)
                {
                    if (isdied == true)
                    {
                        boss_C_explosion_effect.SetActive(false);
                        break;
                    }

                    
                    

                    if (ir != null)
                    {
                        float ir_maxHP = ir.GetComponent<iroomae_subdivide>().max_HP;
                        ir.GetComponent<iroomae_subdivide>().Damaged(ir_maxHP * DamagePercent);
                    }
                }
                boss_C_explosion_effect.GetComponent<magician_attack>().iroomae.Clear();


                picked_effect.SetActive(false);

                yield return new WaitForSeconds(attvelo * 0.4f);
                boss_C_explosion_effect.SetActive(false);
                
                yield return new WaitForSeconds(attvelo * 0.5f);
            }

            

        }



        if (iroomae_Subdivide == null)
        {
            if (anim != null)
                anim.SetInteger("state", 0);
        }
        onattack = false;
    }

    bool isdied_check = false;
    public void Damaged(float damage)
    {
        e_HP -= damage;
        if (e_HP <= 0)
        {
            e_HP = 0;
            if (name != "portal" || name == "enemy_boss")
            {
                // 죽은상태에서도 이펙트 지속되는 버그 없애기
                if (effect != null)
                    effect.SetActive(false);
                if (effect2 != null)
                    effect2.SetActive(false);
                if (effect3 != null)
                    effect3.SetActive(false);
                if (effect4 != null)
                    effect4.SetActive(false);

                isdied = true;

                if (hpbar != null)
                    hpbar.value = 0;

                if (isdied_check == false)
                {
                    gameObject.layer = 7;
                    StartCoroutine(DestroyMotion());
                    isdied_check = true;
                }
            }
            else if(name == "portal")
            {
                hpbar.value = 0;
            }
        }
    }
    IEnumerator DestroyMotion()
    {

        anim.SetInteger("state", 4);

        
        float duration = 0.5f;
        float time = 0.0f;
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.right * 1.5f;


        //적 회전
        transform.localEulerAngles = new Vector3(0, 0, -30);


        //적 통통 튕기는 모션
        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, 2, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);
            yield return null;

        }
        duration = 0.3f;
        time = 0f;
        start = transform.position;
        end = transform.position + Vector3.right * 1f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, 1, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            yield return null;
        }
        duration = 0.15f;
        time = 0f;
        start = transform.position;
        end = transform.position + Vector3.right * 0.5f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, 0.5f, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            yield return null;
        }


        if (name != "enemy_boss")
            Destroy(gameObject, 0.5f);
        else
            transform.localEulerAngles = new Vector3(0, 0, 0);

    }
}
