using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiEnemyScript : MonoBehaviour
{
    public float lookRadius = 10f;
    private Animator anim;
    public bool attacking = false;
    public Health oneAttack; 

    Transform target;
    NavMeshAgent agent;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            attacking = false;
            float distance = Vector3.Distance(target.position, transform.position);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
            {
                agent.speed = 0;
                attacking = true;
            }
            else
                agent.speed = 3.5f;

            if (distance <= lookRadius)
            {
                anim.SetBool("walk",true);
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance)
                {
                    oneAttack.gotHitFlag = false;
                    anim.SetTrigger("attack");
                    //Attack
                    FaceTarget();
                }
            }
            else
                anim.SetBool("walk", false);
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}

