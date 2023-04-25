using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void Update()
    {
    }
    bool isHit = false;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거1");

        
        if (other.tag == "Monster" && !isHit)
        {
            Debug.Log("트리거2");
            EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
            enemyAI.Damaged();
            isHit = true;
            StartCoroutine(hitTime());

            // 맞은 놈의 스크립트를 가져와서.
            // 그놈의 체력을 깍는다.
        }
    }
    IEnumerator hitTime()
    {
        yield return new WaitForSeconds(PlayerStats.AttackSpeed);
        isHit = false;

    }


     
}
