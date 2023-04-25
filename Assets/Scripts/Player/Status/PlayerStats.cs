using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int currentLevel { get; protected set; } = 1;       // 현제 레벨
    [SerializeField]
    public static int maxLevel = 100;     // 최대 레벨

    public static float maxHP { get; protected set; } = 100;        // 전체 체력
    public static float currentHP { get; protected set; } = 50;    // 현제 체력
    public static float HP_RegenPerSecond { get; protected set; } = 0.1f;   // 초당 체력 회복량


    public static float maxMP { get; protected set; } = 100;      // 전체 마나
    public static float currentMP { get; protected set; } = 50;      // 전체 마나
    public static float MP_RegenPerSecond { get; protected set; } = 0.1f;   // 초당 마나 회복량


    public static float maxStaminaPoint { get; protected set; } = 100;       // 전체 스테미나
    public static float currentStaminaPoint { get; protected set; } = 50;   // 현제 스테미나
    public static float SP_RegenPerSecond { get; protected set; } = 2f;   // 초당 스테미나 회복량

    public static float attack { get; protected set; } = 10;           // 공격력
    public static float weaponAttack { get; protected set; }       // 무기 공격력
    public static float AttackSpeed { get; protected set; } = 1f;     // 공격 속도
    public static float defence { get; protected set; } = 0;  // 방어력
    public static float armorDefence { get; protected set; } // 방어구 방어력
    public static float moveSpeed { get; protected set; } = 5f; // 속도

    // 총 경험치
    // 레벨에서의 현제 경험치
    // 레벨에서의 총 경험치

    public static int totalExp { get; protected set; }           // 총 경험치
    public static int currentLevelExp { get; protected set; }    // 레벨에서의 현제 경험치        
    public static int maxLevelExp { get; protected set; }        // 레벨에서의 총 경험치


    public static int gold { get; protected set; }               // 골드
    public float expMultiplier = 1.2f;

    // 레벨당 요구 경험치 배열
    public static int[] requiredExpPerLevel;
}
