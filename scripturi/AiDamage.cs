using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDamage : MonoBehaviour
{
    public int damageApply = 10;
    public AIHealth healthBar;
    public AiEnemyScript attackFlag;
    public Health enemyHitStatus;

    public bool AICollisionFlag = false;
    public Collider m_Collider;

    void Update()
    {
        if (attackFlag.attacking == true)
        {
            m_Collider.enabled = true;
        }
        else
        {
            m_Collider.enabled = false;
        }
    }

    void LateUpdate()
    {
        AICollisionFlag = false;
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player" && enemyHitStatus.gotHitFlag == false)
        {
            AICollisionFlag = true;
        }
    }
}