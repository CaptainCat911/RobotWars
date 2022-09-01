using UnityEngine;
using UnityEngine.AI;

public class Enemy : Fighter
{
    NavMeshAgent navMeshAgent;
    Player player;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameManager.instance.player;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        if (distance < 5.1f)
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }



    void Fire()
    {
        Debug.Log("Fire");
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
}
