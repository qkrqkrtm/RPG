using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController02 : MonoBehaviour
{
    float moveSpeed = 5.0f;
    NavMeshAgent nav;

    // 마우스 우클릭하면 마우스에서 레이저가 나와서
    // 레이저 닿은 곳으로 이동한다.
    // 이동 코루틴으로 이동을 구현하며
    // 코루틴이 있을 시 취소하고 다음 이동코루틴을 구현한다.


    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // 좌클릭시 실행
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //카메라에서 스크린으로 나오는 레이를 최대거리100f만큼 쏴서.
            //맞은 백터를 MoveTo함수로 보냄.
            Physics.Raycast(ray, out hit,100f);
            

            MoveTo(hit.point);
        }
    }

    // MoveTo함수에서 코르틴 실행을 멈추고 새로운 목적지를 설정하고
    // 이동 코르틴을 실행
    void MoveTo(Vector3 destination)
    {
        StopCoroutine("MovingToDestination");

        nav.destination = destination;
        nav.speed = moveSpeed;

        StartCoroutine("MovingToDestination");
    }

    // 거리가 0.1미만이면 도착
    IEnumerator MovingToDestination()
    {
        if (Vector3.Distance(transform.position, nav.destination) < 0.1)
        {
            transform.position =  nav.destination;
            yield return null;
        }
    }
}
