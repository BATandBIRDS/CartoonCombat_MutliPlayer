using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : NetworkBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float turtleDamage = 15f;
    [SerializeField] float healerGain = -15f;
    [SerializeField] Slider inGameHealthBar;

    //Slider healthBar;
    Animator animator;
    float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        //healthBar = GameObject.FindGameObjectWithTag("healthBar").GetComponent<Slider>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        UpdateHealthBar();
    }

    public float GetHealth { get { return currentHealth; } }

    public void GetHit(float enemyDamage)
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        
        if (pm.IsDefending) 
        { 
            enemyDamage -= pm.GetDefend;                                 // defend penatly.
            StaminaSystem staminaSystem = GetComponent<StaminaSystem>(); // You get hit while defending, You also spend some stamina.
            staminaSystem.SpendHitPenaltyStamina();                      // Everything has a coast isn't it?
        }
        else
        {
            pm.ImHit();   // STUN!
        }

        if (currentHealth >= 0) 
        {
            currentHealth -= enemyDamage;
            animator.SetTrigger("getHit");
        }
    }

    void UpdateHealthBar()
    {
        //healthBar.value = currentHealth / maxHealth;
        inGameHealthBar.value = currentHealth / maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("turtle"))
        {
            GetHit(turtleDamage);
        }
        else if (other.gameObject.CompareTag("healer"))
        {
            GetHit(healerGain);
            Destroy(other.gameObject);
        }
    }
}
