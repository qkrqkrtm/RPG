using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MonsterSO : ScriptableObject
{
    // 몬스터 레벨
    [SerializeField]
    public float LV = 1f;
    // 체력, 최대체력
    [SerializeField]
    public float HP = 100f;
    [SerializeField]
    public float MaxHP = 100f;

    // 공격력, 방어력
    [SerializeField]
    public float Attack = 10f;
    [SerializeField]
    public float Defence = 1f;

    // 이동 속도
    [SerializeField]
    public float Speed = 3.5f;


    // 추격 거리, 추격 해제 거리
    [SerializeField]
    public float ChaseDistance = 5f;
    [SerializeField]
    public float ChaseStopDistance = 20f;


    // 공격 속도, 공격 범위
    [SerializeField]
    public float AttackSpeed = 2f;
    [SerializeField]
    public float AttackRange;

    // 획득 경험치, 골드
    [SerializeField]
    public float GainEXP = 30f;
    [SerializeField]
    public float GainGold = 100f;
    // 드랍 아이템
}
