using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent enemy;

    // �ݺ� ���� ����
    // ������ ���ٰ�
    // Player�� �߰��ϸ� ����ȣ���Լ��� ���߰� �÷��̾ target���� �����Ѵ�.

    // �÷��̾ target���� �����ϴ� ������ ���� ������ �������� Ž���ؼ�
    // �� Ž���� ���� �ȿ� �÷��̾ ������ Ž���� ���� target�� �־��ش�.
    // ������Ʈ ���� SetDestination �÷��̾ �����Ѵ�.



    // �̵� �ӵ�
    // ���� �ӵ�
    // �߰� �Ÿ�
    // �߰� ���� �Ÿ�


    [SerializeField]
    Transform[] WayPoints;
    int N;
    int M=0;
    Animator anim;
    [SerializeField]
    Transform player;
    Transform target;

    float distance;

    //// �÷��̾� �߰ݰŸ�
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
        // 0�ʰ����� �ΰ� 2�ʸ��� �ݺ�ȣ��
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

        // �Ÿ��� 5������ ������ �߰�
        if (distance < monsterStat.ChaseDistance)
        {
            SetTarget(player);
        }

        // �Ÿ��� 20���� �־����� �߰ݸ���
        if (distance > monsterStat.ChaseStopDistance)
        {
            RemoveTarget();
        }
    }

    // ���Ͱ� �������� �޾��� �� HP����
    public void Damaged()
    {
        monsterStat.HP -= PlayerStats.attack;
        Debug.Log(monsterStat.HP);
    }

    // �÷��̾� �߰� ��, Ž�� ����--> �߰�.
    // �÷��̾� ��� ��, �߰� ����--> Ž��.
    public void SetTarget(Transform _target)
    {
        CancelInvoke();
        
        target = _target;

        if (PlayerController3D.isDie)
            RemoveTarget();
    }

    // Ÿ�� ���� ����.
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
