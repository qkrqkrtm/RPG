using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : EnemyAI
{
    bool isAttacking;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !isAttacking)
        {
            StartCoroutine(MonsterAttack());
            Debug.Log(PlayerStats.currentHP);
        }
    }

    
    // 몬스터의 공격속도마다 피격
    IEnumerator MonsterAttack()
    {
        PlayerStatsController.Instance_PlayerStatsController.PlayerDamaged(monsterStat.Attack);
        isAttacking = true;
        yield return new WaitForSeconds(monsterStat.AttackSpeed);
        isAttacking = false;
    }
}
