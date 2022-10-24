using UnityEngine;
using UnityEngine.AI;

public class Enemy : Fighter
{
    NavMeshAgent agent;
    Animator animator;
    [HideInInspector]
    public GameObject target;                       // ����
    [HideInInspector]
    public bool targetVisible;                      // ����� �� ���� ��� ���
    [HideInInspector] 
    public bool readyToFire;                        // ����� ��������

    public float distanceToShoot;                   // ���������, � ������� ����� ��������
    public float faceingTargetSpeed;                // �������� �������� 


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameManager.instance.player.gameObject;        // ���� ��� ���� ������ �����
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        if (!target)
            return;

        NavMeshHit hit;
        if (!agent.Raycast(target.transform.position, out hit))
        {
            //Debug.Log("Visible");            
            targetVisible = true;                                       // Target is "visible" from our position.
        }
        else
        {
            // ��� �������� �������� ����� ������ ����� ��� ������� (����� ��� ������ �������� ��� ����)
            targetVisible = false;
        }

        float distance = Vector3.Distance(transform.position, target.transform.position);       // ������� ��������� �� ����
        if (distance < distanceToShoot && targetVisible)                                        // ���� ����� �� ���� � ����� �
        {
            agent.ResetPath();                                                                  // ���������� ����
            FaceTarget();                                                                       // ��������������� � ���
            readyToFire = true;                                                                 // ����� ��������
            animator.SetBool("Runing", false);                                                   // �������� ��� (����� ����������)
        }
        else
        {
            agent.SetDestination(target.transform.position);                                    // ������������ � ����
            readyToFire = false ;                                                               // �� ����� ��������
            animator.SetBool("Runing", true);                                                   // �������� ��� (����� ����������)
        }
    }       


    void FaceTarget()                                                                           // ������� � ����
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * faceingTargetSpeed);
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
}
