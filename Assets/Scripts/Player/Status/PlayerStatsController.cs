using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Diagnostics;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerStatsController : PlayerStats
{
    public static PlayerStatsController Instance_PlayerStatsController = null;

    bool isRegening;
    
    GameObject Player;
    Animator anim;
    PlayerController3D playerController3D;

    // �̱��� ��ũ��Ʈ.
    // �ٸ� ��ũ��Ʈ���� set ����.



    private void Awake()
    {
        if (Instance_PlayerStatsController == null)
            Instance_PlayerStatsController = this;
        else if (Instance_PlayerStatsController != null)
            Destroy(this.gameObject);

        Player = GameObject.Find("Solus_The_Knight");

        anim = Player.GetComponent<Animator>();

        playerController3D = Player.GetComponent<PlayerController3D>();
    }

   

    // ����ġ ����.
    private void Start()
    {
        // ������ �䱸 ����ġ �迭 �ʱ�ȭ
        PlayerStats.requiredExpPerLevel = new int[PlayerStats.maxLevel];

        PlayerStats.requiredExpPerLevel[0] = 100;

        // ������ �䱸 ����ġ ��� �� �迭�� ����
        for (int i = 1; i < PlayerStats.maxLevel; i++)
        {
            // �Ҽ��� �Ʒ� ù ��° �ڸ��� �������� �ݿø�
            PlayerStats.requiredExpPerLevel[i] = Mathf.RoundToInt(PlayerStats.requiredExpPerLevel[i - 1] * expMultiplier);
        }
    }

    
    // ������ �Լ��� ����� ��, ������ ����ϵ��� �Լ�.
    public void CurrentStats(int LV)
    {
        LV -= 1;
        PlayerStats.maxHP = 100 + (LV * 20);
        PlayerStats.maxMP = 100 + (LV * 20);
        PlayerStats.maxStaminaPoint = 100 + (LV * 2);
        PlayerStats.attack = 10 + (LV * 2);
        PlayerStats.defence = 0 + (LV * 2);
        PlayerStats.MP_RegenPerSecond = 0.1f + (LV * 0.1f);
        PlayerStats.HP_RegenPerSecond = 0.1f + (LV * 0.1f);
        PlayerStats.SP_RegenPerSecond = 2f + (LV * 0.1f);
    }

    // HP MP SP �ʴ� ������ ��ŭ �����Ǵ� �Լ�. PlayerController3D���� ������Ʈ.
    public void RegenHPMPSP()
    {
        if(!isRegening)
            StartCoroutine(Regen());
        // ��� �ʰ���.
        if (PlayerStats.maxHP < PlayerStats.currentHP)
        {
            PlayerStats.currentHP = PlayerStats.maxHP;
        }

        if (PlayerStats.maxMP < PlayerStats.currentMP)
        {
            PlayerStats.currentMP = PlayerStats.maxMP;
        }

        if (PlayerStats.maxStaminaPoint < PlayerStats.currentStaminaPoint)
        {
            PlayerStats.currentStaminaPoint = PlayerStats.maxStaminaPoint;
        }
    }

    // 0.1�ʴ� �������� 0.1��ŭ�� ȸ��.
    IEnumerator Regen()
    {
        PlayerStats.currentHP += PlayerStats.HP_RegenPerSecond *1f;
        PlayerStats.currentMP += PlayerStats.MP_RegenPerSecond * 1f;
        PlayerStats.currentStaminaPoint += PlayerStats.SP_RegenPerSecond * 1f;
        isRegening = true;
        yield return new WaitForSeconds(1f);
        isRegening = false;
    }

    // currentLevel 1����.
    // ���� ���.
    public void LevelUP()
    {
        PlayerStats.currentLevel++;
        CurrentStats(PlayerStats.currentLevel);
    }

    public static void HP_Recovery(float value)
    {
        PlayerStatsController.currentHP += value;

        if(PlayerStatsController.currentHP > PlayerStatsController.maxHP) PlayerStatsController.currentHP = PlayerStatsController.maxHP;
    }

    // exp��ŭ�� ����ġ ȹ��.
    public void GainExp(int exp)
    {
        PlayerStats.totalExp += exp;
        PlayerStats.currentLevelExp += exp;

        while (PlayerStats.currentLevelExp >= PlayerStats.requiredExpPerLevel[PlayerStats.currentLevel - 1])
        {
            PlayerStats.currentLevelExp -= PlayerStats.requiredExpPerLevel[PlayerStats.currentLevel - 1];
            LevelUP();
        }
    }

    // SP��ŭ currentStaminaPoint �Һ�.
    public void Spend_SP(int SP)
    {
        
        PlayerStats.currentStaminaPoint -= 10;
        if (PlayerStats.currentStaminaPoint < 0) PlayerStats.currentStaminaPoint = 0;
    }

    // ����� �� �Լ�
    public void Exp_Plus_100()
    {
        GainExp(100);
        Debug.Log("currentLevel" + PlayerStats.currentLevel);
        Debug.Log("totalExp" + PlayerStats.totalExp);
        Debug.Log("currentLevelExp " + PlayerStats.currentLevelExp);
        Debug.Log("requiredExpPerLevel " + PlayerStats.requiredExpPerLevel[PlayerStats.currentLevel - 1]);
    }

    public void HP_P()
    {
        if(PlayerStats.currentHP < PlayerStats.maxHP)
        {
            PlayerStats.currentHP += 10;
        }
    }

    public void HP_M()
    {
        if(0 < PlayerStats.currentHP)
        {
            PlayerStats.currentHP -= 10;
        }
    }

    // �÷��̾� �ǰ� �Լ�. damage��ŭ.
    // ����� ��� �Լ�.
    public void PlayerDamaged(float damage)
    {
        if (PlayerController3D.isDie) return;
        PlayerStats.currentHP -= damage;
        if(PlayerStats.currentHP <= 0)
        {
            PlayerStats.currentHP = 0;
            
            // ���� ó��.
            PlayerDIe();
            // hpmpsp���� �ȵǰ� ����.
        }
    }

    private void Update()
    {
        //Debug.Log(PlayerController3D.isDie);
    }

    // ��� �Լ�.
    public void PlayerDIe()
    {
        Debug.Log("��������");

        if (!PlayerController3D.isDie)
            anim.SetBool("IsDie", true);
        PlayerController3D.isDie = true; 
    }

    //�÷��̾� �һ�.
    public void Revive()
    {
        Debug.Log("�һ�");
        PlayerStats.currentHP = PlayerStats.maxHP * 0.7f;
        PlayerStats.currentMP = PlayerStats.maxMP * 0.7f;
        PlayerStats.currentStaminaPoint = PlayerStats.maxStaminaPoint * 0.7f;
        playerController3D.gameObject.transform.position = new Vector3(0, 1, 0);
        
    }

    public IEnumerator CO_Revive(float time)
    {
        Revive();
        yield return new WaitForSeconds(time-1);
        anim.SetBool("IsDie", false);
        yield return new WaitForSeconds(1);
        anim.CrossFade("Movement", 0.5f);
        PlayerController3D.isDie = false;
    }
}
