using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public damage damageChecker;

    public int maxHealth = 100;
    public int currentHealth = 100;
    public ParticleSystem hitEffect;
    public AudioSource hitSound;
    public bool gotHitFlag = false;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        damageChecker.collisionFlag = false;
        anim = GetComponentInChildren<Animator>();
        this.currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            anim.SetTrigger("died");
        }

        if (damageChecker.collisionFlag == true)
        {
            anim.SetTrigger("gotHit");
            hitEffect.Play();
            hitSound.Play();
            hitSound.pitch = Random.Range(0.5f,0.8f);
            GetComponent<AIHealth>().TakeDmg(damageChecker.damageApply);
            gotHitFlag = true;
        }
    }

    void TakeDmg(int damage)
    {
        currentHealth -= damage;
    }
}
