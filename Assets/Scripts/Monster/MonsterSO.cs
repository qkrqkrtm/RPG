using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MonsterSO : ScriptableObject
{
    // ���� ����
    [SerializeField]
    public float LV = 1f;
    // ü��, �ִ�ü��
    [SerializeField]
    public float HP = 100f;
    [SerializeField]
    public float MaxHP = 100f;

    // ���ݷ�, ����
    [SerializeField]
    public float Attack = 10f;
    [SerializeField]
    public float Defence = 1f;

    // �̵� �ӵ�
    [SerializeField]
    public float Speed = 3.5f;


    // �߰� �Ÿ�, �߰� ���� �Ÿ�
    [SerializeField]
    public float ChaseDistance = 5f;
    [SerializeField]
    public float ChaseStopDistance = 20f;


    // ���� �ӵ�, ���� ����
    [SerializeField]
    public float AttackSpeed = 2f;
    [SerializeField]
    public float AttackRange;

    // ȹ�� ����ġ, ���
    [SerializeField]
    public float GainEXP = 30f;
    [SerializeField]
    public float GainGold = 100f;
    // ��� ������
}
