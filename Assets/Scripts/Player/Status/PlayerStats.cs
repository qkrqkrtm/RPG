using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int currentLevel { get; protected set; } = 1;       // ���� ����
    [SerializeField]
    public static int maxLevel = 100;     // �ִ� ����

    public static float maxHP { get; protected set; } = 100;        // ��ü ü��
    public static float currentHP { get; protected set; } = 50;    // ���� ü��
    public static float HP_RegenPerSecond { get; protected set; } = 0.1f;   // �ʴ� ü�� ȸ����


    public static float maxMP { get; protected set; } = 100;      // ��ü ����
    public static float currentMP { get; protected set; } = 50;      // ��ü ����
    public static float MP_RegenPerSecond { get; protected set; } = 0.1f;   // �ʴ� ���� ȸ����


    public static float maxStaminaPoint { get; protected set; } = 100;       // ��ü ���׹̳�
    public static float currentStaminaPoint { get; protected set; } = 50;   // ���� ���׹̳�
    public static float SP_RegenPerSecond { get; protected set; } = 2f;   // �ʴ� ���׹̳� ȸ����

    public static float attack { get; protected set; } = 10;           // ���ݷ�
    public static float weaponAttack { get; protected set; }       // ���� ���ݷ�
    public static float AttackSpeed { get; protected set; } = 1f;     // ���� �ӵ�
    public static float defence { get; protected set; } = 0;  // ����
    public static float armorDefence { get; protected set; } // �� ����
    public static float moveSpeed { get; protected set; } = 5f; // �ӵ�

    // �� ����ġ
    // ���������� ���� ����ġ
    // ���������� �� ����ġ

    public static int totalExp { get; protected set; }           // �� ����ġ
    public static int currentLevelExp { get; protected set; }    // ���������� ���� ����ġ        
    public static int maxLevelExp { get; protected set; }        // ���������� �� ����ġ


    public static int gold { get; protected set; }               // ���
    public float expMultiplier = 1.2f;

    // ������ �䱸 ����ġ �迭
    public static int[] requiredExpPerLevel;
}
