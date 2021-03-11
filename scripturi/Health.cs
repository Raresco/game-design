using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AiDamage damageChecker;
    public HeatlthBarScript healthBar;
    public ParticleSystem hitEffect;
    public AudioSource hitSound;
    public bool gotHitFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    { 
        if (damageChecker.AICollisionFlag == true)
        {
            TakeDamage(damageChecker.damageApply);
            hitEffect.Play();
            hitSound.Play();
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
