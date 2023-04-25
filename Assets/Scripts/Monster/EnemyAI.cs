using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent enemy;

    // 반복 구역 설정
    // 순찰을 돌다가
    // Player를 발견하면 순찰호출함수를 멈추고 플레이어를 target으로 설정한다.

    // 플레이어를 target으로 설정하는 기준은 몬스터 주위에 구형으로 탐지해서
    // 그 탐지한 범위 안에 플레이어가 들어오면 탐지한 놈을 target에 넣어준다.
    // 업데이트 문의 SetDestination 플레이어를 실행한다.



    // 이동 속도
    // 공격 속도
    // 추격 거리
    // 추격 해제 거리


    [SerializeField]
    Transform[] WayPoints;
    int N;
    int M=0;
    Animator anim;
    [SerializeField]
    Transform player;
    Transform target;

    float distance;

    //// 플레이어 추격거리
    //[SerializeField]
    //float chaseDistance = 5f;
    //[SerializeField]
    //float chaseStopDistance = 20f;
    //[SerializeField]
    //float mosterSpeed = 20f;

    [SerializeField]
    protected MonsterSO monsterStat;

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemy.speed = monsterStat.Speed;
        // 0초간격을 두고 2초마다 반복호출
        InvokeRepeating("MoveToNextPoint", 0f, 2f);
    }

   
    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        //Debug.Log(distance);
        anim.SetFloat("isMove", enemy.velocity.magnitude);

        if(target != null)
        {
            enemy.SetDestination(target.position);
        }

        // 거리가 5안으로 들어오면 추격
        if (distance < monsterStat.ChaseDistance)
        {
            SetTarget(player);
        }

        // 거리가 20으로 멀어지면 추격멈춤
        if (distance > monsterStat.ChaseStopDistance)
        {
            RemoveTarget();
        }
    }

    // 몬스터가 데미지를 받았을 때 HP감소
    public void Damaged()
    {
        monsterStat.HP -= PlayerStats.attack;
        Debug.Log(monsterStat.HP);
    }

    // 플레이어 발견 시, 탐색 중지--> 추격.
    // 플레이어 사망 시, 추격 금지--> 탐색.
    public void SetTarget(Transform _target)
    {
        CancelInvoke();
        
        target = _target;

        if (PlayerController3D.isDie)
            RemoveTarget();
    }

    // 타겟 설정 종료.
    public void RemoveTarget()
    {
        target = null;
        InvokeRepeating("MoveToNextPoint", 0f, 2f);
    }

    protected void MoveToNextPoint()
    {
        if(target == null)
        {
            if (enemy.velocity.magnitude < 0.1f)
            {
                

                while (true)
                {
                    N = Random.Range(0, WayPoints.Length);
                    if (N != M)
                    {
                        break;
                    }
                }
                M = N;
                enemy.stoppingDistance = 0.1f;
                enemy.SetDestination(WayPoints[N].position);
            }
        }
    }
}
