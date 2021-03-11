using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    public int damageApply = 10;
    public Health healthBar;
    public ThirdPersonMovement attackFlag;

    public AIHealth enemyHitStatus;
    public bool collisionFlag = false;
    public Collider m_Collider;
    public GameObject impactParticle;
    public Vector3 impactNormal;

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
        collisionFlag = false;
    }

    void OnTriggerEnter(Collider enemy)
    {
        if(enemy.tag == "enemy" && enemyHitStatus.gotHitFlag == false )
        {
            collisionFlag = true;
        }
    }
}
