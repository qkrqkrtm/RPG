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

    // 싱글턴 스크립트.
    // 다른 스크립트에서 set 금지.



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

   

    // 경험치 설정.
    private void Start()
    {
        // 레벨당 요구 경험치 배열 초기화
        PlayerStats.requiredExpPerLevel = new int[PlayerStats.maxLevel];

        PlayerStats.requiredExpPerLevel[0] = 100;

        // 레벨별 요구 경험치 계산 및 배열에 저장
        for (int i = 1; i < PlayerStats.maxLevel; i++)
        {
            // 소수점 아래 첫 번째 자리를 기준으로 반올림
            PlayerStats.requiredExpPerLevel[i] = Mathf.RoundToInt(PlayerStats.requiredExpPerLevel[i - 1] * expMultiplier);
        }
    }

    
    // 레벨업 함수가 실행될 때, 스탯을 상승하도록 함수.
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

    // HP MP SP 초당 리젠량 만큼 리젠되는 함수. PlayerController3D에서 업데이트.
    public void RegenHPMPSP()
    {
        if(!isRegening)
            StartCoroutine(Regen());
        // 재생 초과시.
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

    // 0.1초당 리젠량의 0.1만큼이 회복.
    IEnumerator Regen()
    {
        PlayerStats.currentHP += PlayerStats.HP_RegenPerSecond *1f;
        PlayerStats.currentMP += PlayerStats.MP_RegenPerSecond * 1f;
        PlayerStats.currentStaminaPoint += PlayerStats.SP_RegenPerSecond * 1f;
        isRegening = true;
        yield return new WaitForSeconds(1f);
        isRegening = false;
    }

    // currentLevel 1증가.
    // 스탯 상승.
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

    // exp만큼의 경험치 획득.
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

    // SP만큼 currentStaminaPoint 소비.
    public void Spend_SP(int SP)
    {
        
        PlayerStats.currentStaminaPoint -= 10;
        if (PlayerStats.currentStaminaPoint < 0) PlayerStats.currentStaminaPoint = 0;
    }

    // 디버그 용 함수
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

    // 플레이어 피격 함수. damage만큼.
    // 사망시 사망 함수.
    public void PlayerDamaged(float damage)
    {
        if (PlayerController3D.isDie) return;
        PlayerStats.currentHP -= damage;
        if(PlayerStats.currentHP <= 0)
        {
            PlayerStats.currentHP = 0;
            
            // 죽음 처리.
            PlayerDIe();
            // hpmpsp리젠 안되게 설정.
        }
    }

    private void Update()
    {
        //Debug.Log(PlayerController3D.isDie);
    }

    // 사망 함수.
    public void PlayerDIe()
    {
        Debug.Log("다이힘스");

        if (!PlayerController3D.isDie)
            anim.SetBool("IsDie", true);
        PlayerController3D.isDie = true; 
    }

    //플레이어 소생.
    public void Revive()
    {
        Debug.Log("소생");
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
