using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController02 : MonoBehaviour
{
    float moveSpeed = 5.0f;
    NavMeshAgent nav;

    // ���콺 ��Ŭ���ϸ� ���콺���� �������� ���ͼ�
    // ������ ���� ������ �̵��Ѵ�.
    // �̵� �ڷ�ƾ���� �̵��� �����ϸ�
    // �ڷ�ƾ�� ���� �� ����ϰ� ���� �̵��ڷ�ƾ�� �����Ѵ�.


    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // ��Ŭ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //ī�޶󿡼� ��ũ������ ������ ���̸� �ִ�Ÿ�100f��ŭ ����.
            //���� ���͸� MoveTo�Լ��� ����.
            Physics.Raycast(ray, out hit,100f);
            

            MoveTo(hit.point);
        }
    }

    // MoveTo�Լ����� �ڸ�ƾ ������ ���߰� ���ο� �������� �����ϰ�
    // �̵� �ڸ�ƾ�� ����
    void MoveTo(Vector3 destination)
    {
        StopCoroutine("MovingToDestination");

        nav.destination = destination;
        nav.speed = moveSpeed;

        StartCoroutine("MovingToDestination");
    }

    // �Ÿ��� 0.1�̸��̸� ����
    IEnumerator MovingToDestination()
    {
        if (Vector3.Distance(transform.position, nav.destination) < 0.1)
        {
            transform.position =  nav.destination;
            yield return null;
        }
    }
}
