using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iroomae_subdivide : MonoBehaviour
{
    RaycastHit2D hitInfo;
    VariablesManager variables;
    bool onattack = false;

    // 이루매 변수 모음
    public float iroomae_HP;
    public float iroomae_att;
    public float iroomae_attvelo;
    public float iroomae_dis;

    float iroomae_speed = 2;
    static int iroomae_order = 1;
    SpriteRenderer spriteRenderer;


    //애니메이션 변수
    public Animator anim;

    // 이펙트 오브젝트 모음
    public GameObject effect;
    public GameObject effect2;
    public GameObject effect3;

    //죽음 이펙트 관련
    public GameObject d_effect;
    public AnimationCurve curve;

    public bool isdied = false;



    // 학관 변수
    public Transform student_union;
    public Slider hpbar;
    public float max_HP;

    //
    public Transform MyCharacters;      // 아군 캐릭터들 폴더 오브젝트
    void Awake()
    {
        GameObject VM = GameObject.Find("Variables_Manager");
        GameObject SM = GameObject.Find("SoundManager");
        variables = VM.GetComponent<VariablesManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = iroomae_order;
        iroomae_order += 1;

    }
    void Start()
    {
        if(name != "Student_Union")
        {
            anim.SetInteger("state", 0);
        }

        // 탱커 3종 수치 설정
        if (name == "iroomae_tanker1(Clone)" || name == "iroomae_tanker1")
        {
            iroomae_HP = variables.tanker_var1[0];
            iroomae_att = variables.tanker_var1[1];
            iroomae_attvelo = variables.tanker_var1[2];
            iroomae_dis = variables.tanker_var1[3];
        }
        else if (name == "iroomae_tanker2(Clone)" || name == "iroomae_tanker2")
        {
            iroomae_HP = variables.tanker_var2[0];
            iroomae_att = variables.tanker_var2[1];
            iroomae_attvelo = variables.tanker_var2[2];
            iroomae_dis = variables.tanker_var2[3];
        }
        else if (name == "iroomae_tanker3(Clone)" || name == "iroomae_tanker3")
        {
            iroomae_HP = variables.tanker_var3[0];
            iroomae_att = variables.tanker_var3[1];
            iroomae_attvelo = variables.tanker_var3[2];
            iroomae_dis = variables.tanker_var3[3];
        }
        // 검사 3종 수치 설정
        else if (name == "iroomae_swordman1(Clone)" || name == "iroomae_swordman1")
        {
            iroomae_HP = variables.swordman_var1[0];
            iroomae_att = variables.swordman_var1[1];
            iroomae_attvelo = variables.swordman_var1[2];
            iroomae_dis = variables.swordman_var1[3];
        }
        else if (name == "iroomae_swordman2(Clone)" || name == "iroomae_swordman2")
        {
            iroomae_HP = variables.swordman_var2[0];
            iroomae_att = variables.swordman_var2[1];
            iroomae_attvelo = variables.swordman_var2[2];
            iroomae_dis = variables.swordman_var2[3];
        }
        else if (name == "iroomae_swordman3(Clone)" || name == "iroomae_swordman3")
        {
            iroomae_HP = variables.swordman_var3[0];
            iroomae_att = variables.swordman_var3[1];
            iroomae_attvelo = variables.swordman_var3[2];
            iroomae_dis = variables.swordman_var3[3];
        }
        // 거너 3종 수치 설정
        else if (name == "iroomae_gunner1(Clone)" || name == "iroomae_gunner1")
        {
            iroomae_HP = variables.gunner_var1[0];
            iroomae_att = variables.gunner_var1[1];
            iroomae_attvelo = variables.gunner_var1[2];
            iroomae_dis = variables.gunner_var1[3];
        }
        else if (name == "iroomae_gunner2(Clone)" || name == "iroomae_gunner2")
        {
            iroomae_HP = variables.gunner_var2[0];
            iroomae_att = variables.gunner_var2[1];
            iroomae_attvelo = variables.gunner_var2[2];
            iroomae_dis = variables.gunner_var2[3];
        }
        else if (name == "iroomae_gunner3(Clone)" || name == "iroomae_gunner3")
        {
            iroomae_HP = variables.gunner_var3[0];
            iroomae_att = variables.gunner_var3[1];
            iroomae_attvelo = variables.gunner_var3[2];
            iroomae_dis = variables.gunner_var3[3];
        }
        // 법사 3종 수치 설정
        else if (name == "iroomae_magician1(Clone)" || name == "iroomae_magician1")
        {
            iroomae_HP = variables.magician_var1[0];
            iroomae_att = variables.magician_var1[1];
            iroomae_attvelo = variables.magician_var1[2];
            iroomae_dis = variables.magician_var1[3];
        }
        else if (name == "iroomae_magician2(Clone)" || name == "iroomae_magician2")
        {
            iroomae_HP = variables.magician_var2[0];
            iroomae_att = variables.magician_var2[1];
            iroomae_attvelo = variables.magician_var2[2];
            iroomae_dis = variables.magician_var2[3];
        }
        else if (name == "iroomae_magician3(Clone)" || name == "iroomae_magician3")
        {
            iroomae_HP = variables.magician_var3[0];
            iroomae_att = variables.magician_var3[1];
            iroomae_attvelo = variables.magician_var3[2];
            iroomae_dis = variables.magician_var3[3];
        }
        // 학관 수치 설정
        else
        {
            iroomae_HP = variables.student_union_HP;
        }
        max_HP = iroomae_HP;
    }

    void Update()
    {
        // 이루매 이동, 공격
        if (name != "Student_Union")
        {
            if(isdied == false)
            {
                Ray_attack(transform.position, iroomae_dis, iroomae_att, iroomae_attvelo);
            }
        }
        else
        {
            hpbar.value = iroomae_HP / max_HP;
        }
    }

    // 이루매 공격, 이동 함수
    public void Ray_attack(Vector3 position, float distance, float damage, float attvelo)
    {
        hitInfo = Physics2D.Raycast(position, Vector2.right, distance, LayerMask.GetMask("enemy"));
        Debug.DrawRay(position, Vector2.right * distance, Color.green);

        if (hitInfo.collider != null)
        {
            transform.position = transform.position;
            if(onattack == false)
            {
                onattack = true;
                if(CompareTag("iroomae_magician"))
                {
                     StartCoroutine(Deal2(hitInfo.collider.GetComponent<enemy>(), damage, attvelo));
                        
                }
                else if (CompareTag("iroomae_gunner"))
                {
                     StartCoroutine(Deal3(hitInfo.collider.GetComponent<enemy>(), damage, attvelo));
                }
                else if (CompareTag("iroomae_tanker"))
                {
                    
                     StartCoroutine(Deal4(hitInfo.collider.GetComponent<enemy>(), damage, attvelo));
                }
                else
                {
                     StartCoroutine(Deal(hitInfo.collider.GetComponent<enemy>(), damage, attvelo));                }
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
                if (anim != null)
                    anim.SetInteger("state", 0);
                transform.position = transform.position + Vector3.right * iroomae_speed * Time.deltaTime;
            }
        }
    }

    IEnumerator Deal(enemy enemy, float damage, float attvelo)
    {
        Vector3 hit_pos;
        hit_pos = enemy.transform.position;
        if (enemy != null)
        {
            if (effect != null)
            {
                effect.SetActive(false);
            }
            // 딜할때 애니메이션
            if (anim != null)
            {
                anim.SetInteger("state", 1);
            }
            yield return new WaitForSeconds(attvelo * 0.2f); //데미지 들어가는 싱크 맞추기 위한 초 지연
            if(isdied == false)
            {

                // 딜 이펙트 효과 넣기
                if (name == "iroomae_swordman2(Clone)" || name == "iroomae_swordman2")
                {
                    effect.SetActive(true);
                    effect.transform.position = hit_pos
                        - new Vector3(enemy.transform.localScale.x / 2f, 0, 0)
                        + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                }
                else if (name == "iroomae_swordman3(Clone)" || name == "iroomae_swordman3")
                {
                    effect.SetActive(true);
                    effect.transform.position = hit_pos
                        + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                }
                // 적에게 데미지 입히기
                // 돌아가는 도중 enemy 사라지면 오류발생, 중간 한번 더 체크하는 로직
                if (isdied == false)
                {
                    if (enemy != null)
                    {
                        if(name == "iroomae_swordman1(Clone)" || name == "iroomae_swordman1")
                        {
                            AudioPlayer.instance.PlaySword1Clip();
                        }
                        else if (name == "iroomae_swordman2(Clone)" || name == "iroomae_swordman2")
                        {
                            if(AudioPlayer.instance != null)
                                AudioPlayer.instance.PlaySword2Clip();
                        }
                        else if (name == "iroomae_swordman3(Clone)" || name == "iroomae_swordman3")
                        {
                            AudioPlayer.instance.PlaySword3Clip();
                        }
                        enemy.GetComponent<enemy>().Damaged(damage);
                    }
                }
            }
        }
        yield return new WaitForSeconds(attvelo*0.8f); // 공격속도에 의한 초 지연
        if (anim != null)
            anim.SetInteger("state", 2); // 공격 대기 상태 애니메이션
        if (enemy == null)
        {
            if (anim != null)
                anim.SetInteger("state", 0);
        }
        if (effect != null)
        {
            effect.SetActive(false);
        }

        onattack = false;
    }

    IEnumerator Deal2(enemy enemy, float damage, float attvelo)
    {
        if(isdied == false)
        {
            if (name == "iroomae_magician1(Clone)" || name == "iroomae_magician1")
            {
                if (anim != null)
                {
                    anim.SetInteger("state", 1);
                }
                yield return new WaitForSeconds(attvelo * 0.19f);
                effect.transform.position = new Vector3(enemy.transform.position.x, -2.7f, 0);
                if(isdied == false)
                {
                    effect.SetActive(true);
                }
                

                yield return new WaitForSeconds(attvelo * 0.01f);

                if(AudioPlayer.instance != null)
                    AudioPlayer.instance.PlayMagician1Clip();
                MagicianMultiAttack(damage, 3);

                yield return new WaitForSeconds(attvelo * 0.4f);
                if (anim != null)
                    anim.SetInteger("state", 2); // 공격 대기 상태 애니메이션
                effect.SetActive(false);

                yield return new WaitForSeconds(attvelo * 0.4f);
                if (enemy == null)
                {
                    if (anim != null)
                        anim.SetInteger("state", 0);
                }
                onattack = false;
            }
            else if (name == "iroomae_magician2(Clone)" || name == "iroomae_magician2")
            {
                if (anim != null)
                {
                    anim.SetInteger("state", 1);
                }
                yield return new WaitForSeconds(attvelo * 0.19f);
                if(isdied == false)
                {
                    effect.SetActive(true);
                }
                effect.transform.position = new Vector3(enemy.transform.position.x, 2.0f, 0);
                yield return new WaitForSeconds(attvelo * 0.01f);

                MagicianMultiAttack(damage, 3);
                if(isdied == false)
                {
                    if (AudioPlayer.instance != null)
                        AudioPlayer.instance.PlayMagician2Clip();
                }

                yield return new WaitForSeconds(attvelo * 0.4f);
                if (anim != null)
                    anim.SetInteger("state", 2); // 공격 대기 상태 애니메이션
                effect.SetActive(false);
                yield return new WaitForSeconds(attvelo * 0.4f);
                if (enemy == null)
                {
                    if (anim != null)
                        anim.SetInteger("state", 0);
                }
                onattack = false;
            }
            else if (name == "iroomae_magician3(Clone)" || name == "iroomae_magician3")
            {
                Vector3 detected_enemy_position = enemy.transform.position;
                effect2.transform.position = transform.position + Vector3.up * 5;
                effect.transform.position = transform.position + Vector3.up * 5;
                effect.transform.localScale = new Vector3(0.3f, 0.3f, 0);

                if (anim != null)
                {
                    anim.SetInteger("state", 1);
                }

                if (isdied == false)
                {
                    if(isdied == false)
                    {
                        effect2.SetActive(true);
                    }

                    yield return new WaitForSeconds(attvelo / 2);

                    if(isdied == false)
                    {
                        effect.SetActive(true);
                    }

                    yield return new WaitForSeconds(attvelo / 3);
                    effect2.SetActive(false);
                    float delta = 0;
                    float duration = 1f;

                    while (delta <= duration)
                    {
                     float t = delta / duration;
                        effect.transform.position = Vector3.Lerp(transform.position + Vector3.up * 5,
                            detected_enemy_position, t);

                        effect.transform.localScale = Vector3.Lerp(new Vector3(0.3f, 0.3f, 0),
                            new Vector3(0.6f, 0.6f, 0), t);

                     delta += Time.deltaTime;
                     yield return null;
                    }
                    effect.SetActive(false);

                    effect3.transform.position = detected_enemy_position;
                    if(isdied == false)
                    {
                        effect3.SetActive(true);
                    }

                    yield return new WaitForSeconds(attvelo / 32);

                    MagicianMultiAttack(damage, 5);
                    if(AudioPlayer.instance != null)
                    {
                        AudioPlayer.instance.PlayMagician3Clip();
                    }

                    yield return new WaitForSeconds(attvelo / 4);
                    effect3.SetActive(false);
                    yield return new WaitForSeconds(attvelo / 4);
                }

                if (enemy == null)
                {
                    if (anim != null)
                        anim.SetInteger("state", 0);
                }
                onattack = false;
            }
        }
        
        
    }
    // 법사 랜덤 다중 공격 구현 함수
    void MagicianMultiAttack(float damage, int maxmobs)
    {
        Transform[] myChildren = this.GetComponentsInChildren<Transform>();
        foreach (Transform child in myChildren)
        {
            if (child.name == "m3" || child.name == "Lazer_blue" || child.name == "Flame_burn")
            {
                List<GameObject> pickedmobs = child.GetComponent<magician_attack>().mobs;
                int numTargets = Mathf.Min(maxmobs, pickedmobs.Count);
                List<GameObject> targets = new List<GameObject>();
                while (targets.Count < numTargets)
                {
                    int randomIndex = Random.Range(0, pickedmobs.Count);
                    GameObject mob = pickedmobs[randomIndex];
                    if (!targets.Contains(mob))
                    {
                        targets.Add(mob);
                        if(mob != null)
                        {
                            mob.GetComponent<enemy>().Damaged(damage);
                        }
                    }
                }
                child.GetComponent<magician_attack>().mobs.Clear();
            }

        }
    }



    IEnumerator Deal3(enemy enemy, float damage, float attvelo)
    {
        if (name == "iroomae_gunner1(Clone)" || name == "iroomae_gunner1")
        {
            Vector3 detected_enemy_position = enemy.transform.position;
            Vector3 arrow_end_vector = new Vector3(detected_enemy_position.x,
                -2f, detected_enemy_position.z);

            float dis = (arrow_end_vector - transform.position).magnitude;


            effect2.transform.position = arrow_end_vector + Vector3.down + Vector3.right;


            anim.SetInteger("state", 1);
            yield return new WaitForSeconds(0.2f);
            anim.SetInteger("state", 2);
            effect.SetActive(true);

            AudioPlayer.instance.PlayGunner1Clip();
            // 화살 날아가게 하는 로직
            if(isdied == false)
            {
                float delta = 0;
                float duration = dis / 100;

                while (delta <= duration)
                {
                    float t = delta / duration;
                    if (enemy != null)
                    {
                        effect.transform.position = Vector3.Lerp(transform.position + Vector3.right * 0.5f,
                            arrow_end_vector, t);
                    }

                    delta += Time.deltaTime;
                    yield return null;
                }


                // 적 데미지 입히기
                if (enemy != null)
                {
                    enemy.GetComponent<enemy>().Damaged(damage);
                }
            }


            effect.SetActive(false);
            if (isdied == false)
                effect2.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            effect2.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        else if (name == "iroomae_gunner2(Clone)" || name == "iroomae_gunner2")
        {
            anim.SetInteger("state", 1);
            effect.transform.position = transform.position + Vector3.right*1.8f + Vector3.down*0.6f;
            yield return new WaitForSeconds(attvelo * 0.2f);
            effect.GetComponent<Renderer>().sortingOrder = iroomae_order;
            if(isdied == false)
                effect.SetActive(true);
            effect2.transform.position = new Vector3(enemy.transform.position.x, -3.4f, 0);
            if (isdied == false)
                effect2.SetActive(true);

            // 적 데미지 입히기
            if (enemy != null)
            {
                AudioPlayer.instance.PlayGunner2Clip();
                enemy.GetComponent<enemy>().Damaged(damage);
            }

            yield return new WaitForSeconds(attvelo * 0.3f);
            effect2.SetActive(false);
            yield return new WaitForSeconds(attvelo * 0.2f);
            effect.SetActive(false);
            anim.SetInteger("state", 2);
            yield return new WaitForSeconds(attvelo * 0.3f);


        }
        else if (name == "iroomae_gunner3(Clone)" || name == "iroomae_gunner3")
        {
            anim.SetInteger("state", 1);

            yield return new WaitForSeconds(attvelo * 0.1f);

            float distance = (enemy.transform.position - transform.position).magnitude;
            effect.transform.localScale = new Vector3(distance, effect.transform.localScale.y, effect.transform.localScale.z);
            effect.transform.position = transform.position + new Vector3(distance / 2 +1, -0.7f, 0);

            effect2.transform.position = effect.transform.position + new Vector3(distance / 2 -2.5f, +0.3f, 0);

            if(isdied == false)
                effect.SetActive(true);
            if (isdied == false)
                effect2.SetActive(true);
            // 적 데미지 입히기
            if (enemy != null)
            {
                AudioPlayer.instance.PlayGunner3Clip();
                enemy.GetComponent<enemy>().Damaged(damage);
            }
            yield return new WaitForSeconds(attvelo * 0.05f);
            effect.SetActive(false);
            yield return new WaitForSeconds(attvelo * 0.45f);
            effect2.SetActive(false);
            anim.SetInteger("state", 2);
            yield return new WaitForSeconds(attvelo * 0.4f);
        }


        if (enemy == null)
        {
            if (anim != null)
                anim.SetInteger("state", 0);
        }
        onattack = false;
        yield return null;
    }

    IEnumerator Deal4(enemy enemy, float damage, float attvelo)
    {
        anim.SetInteger("state", 1);
        yield return new WaitForSeconds(attvelo * 0.1f);
        // 적 데미지 입히기
        if (enemy != null)
        {
            AudioPlayer.instance.PlayHitClip();
            enemy.GetComponent<enemy>().Damaged(damage);
        }
        anim.SetInteger("state", 2);
        yield return new WaitForSeconds(attvelo * 0.9f);


        if (enemy == null)
        {
            if (anim != null)
                anim.SetInteger("state", 0);
        }
        onattack = false;
    }







    bool isdied_check = false;

    public void Damaged(float damage)
    {
        iroomae_HP -= damage;
        if (iroomae_HP <= 0)
        {
            iroomae_HP = 0;

            if (name != "Student_Union")
            {
                // 죽은상태에서도 이펙트 지속되는 버그 없애기
                if (effect != null)
                    effect.SetActive(false);
                if (effect2 != null)
                    effect2.SetActive(false);
                if (effect3 != null)
                    effect3.SetActive(false);

                isdied = true;
                onattack = false;
                if (isdied_check == false)
                {
                    gameObject.layer = 7;
                    StartCoroutine(DestroyMotion());
                    isdied_check = true;
                }
            }
            else
            {
                hpbar.value = 0;
                foreach (Transform child in MyCharacters)   // 캐릭터, 몹 오브젝트 삭제
                {
                    child.gameObject.GetComponent<iroomae_subdivide>().Damaged(10000);
                }
                effect.SetActive(true);
                gameObject.SetActive(false);
            }

                
        }
    }

    IEnumerator DestroyMotion()
    {

        anim.SetInteger("state", 4);

        if (effect != null)
            effect.SetActive(false);
        if (effect2 != null)
            effect2.SetActive(false);
        if (effect3 != null)
            effect3.SetActive(false);

        float duration = 0.5f;
        float time = 0.0f;
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.left * 1.5f;


        //이루매 회전
        transform.localEulerAngles = new Vector3(0, 0, 30);


        //이루매 통통 튕기는 모션
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
        end = transform.position + Vector3.left * 1f;
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
        end = transform.position + Vector3.left * 0.5f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, 0.5f, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            yield return null;
        }


        //이루매 영혼 이펙트
        Destroy(gameObject, 0.5f);
        yield return new WaitForSeconds(0.45f);
        Instantiate(d_effect, transform.position, Quaternion.identity);

    }


}
