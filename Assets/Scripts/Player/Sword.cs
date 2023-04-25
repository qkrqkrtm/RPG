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
        Debug.Log("Ʈ����1");

        
        if (other.tag == "Monster" && !isHit)
        {
            Debug.Log("Ʈ����2");
            EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
            enemyAI.Damaged();
            isHit = true;
            StartCoroutine(hitTime());

            // ���� ���� ��ũ��Ʈ�� �����ͼ�.
            // �׳��� ü���� ��´�.
        }
    }
    IEnumerator hitTime()
    {
        yield return new WaitForSeconds(PlayerStats.AttackSpeed);
        isHit = false;

    }


     
}
