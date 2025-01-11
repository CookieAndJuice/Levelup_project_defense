using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesManager : MonoBehaviour
{
    // 배열 인덱스 설명
    // HP : 0
    // 공격력 : 1
    // 공격속도 : 2
    // 사거리 : 3

    // 이루매 변수
    public float[] tanker_var1 = new float[4];
    public float[] tanker_var2 = new float[4];
    public float[] tanker_var3 = new float[4];

    public float[] swordman_var1 = new float[4];
    public float[] swordman_var2 = new float[4];
    public float[] swordman_var3 = new float[4];

    public float[] gunner_var1 = new float[4];
    public float[] gunner_var2 = new float[4];
    public float[] gunner_var3 = new float[4];

    public float[] magician_var1 = new float[4];
    public float[] magician_var2 = new float[4];
    public float[] magician_var3 = new float[4];


    // 적 변수
    public float[] enemy_01 = new float[4];
    public float[] enemy_02 = new float[4];
    public float[] enemy_03 = new float[4];
    public float[] enemy_04 = new float[4];
    public float[] enemy_05 = new float[4];
    public float[] enemy_06 = new float[4];
    public float[] enemy_07 = new float[4];
    public float[] enemy_08 = new float[4];
    public float[] enemy_special = new float[4];
    public float[] finalRound_C = new float[4];
    public float[] finalRound_F = new float[4];
    public float[] enemy_boss = new float[4];
    



    public int student_union_HP;
    public float[] portal_HP = new float[7];
}
