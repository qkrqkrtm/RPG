using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement02 : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5.0f;
    NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 goalPosition)
    {
        StopCoroutine("OnMove");
        nav.speed = moveSpeed;
        nav.SetDestination(goalPosition);
        StartCoroutine("OnMove");
    }

    IEnumerator OnMove()
    {
        while (true)
        {
            //거리가 0.1미만일때
            if(Vector3.Distance(nav.destination,transform.position) < 0.1f)
            {
                transform.position = nav.destination;
                nav.ResetPath();
                break;
            }

            yield return null;
        }
    }
}
