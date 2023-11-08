using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : NetworkBehaviour
{
    [SerializeField] float maxStamina = 100f;
    [SerializeField] float recoverStaminaRate = 3;
    [SerializeField] float attackStaminaCoast = 20f;
    [SerializeField] float defendStaminaCoast = 15f;
    [SerializeField] float hitPenaltyStamina = 5f;
    [SerializeField] Slider inGameStaminaBar;

    //Slider staminaBar;
    float currentStamina;

    void Awake()
    {
        currentStamina = maxStamina;
        //staminaBar = GameObject.FindGameObjectWithTag("staminaBar").GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        UpdateStaminaBar();
        RecoverStamina();
    }
    public void SpendHitPenaltyStamina()
    {
        currentStamina -= hitPenaltyStamina;
    }

    public void SpendAttackStamina()
    {
        currentStamina -= attackStaminaCoast;
    }

    public void SpendDefendStamina()
    {
        currentStamina -= defendStaminaCoast;
    }

    void RecoverStamina()
    {
        if (currentStamina >= maxStamina) { return; }
        currentStamina += recoverStaminaRate * Time.fixedDeltaTime;
    }

    void UpdateStaminaBar()
    {
        //staminaBar.value = currentStamina / maxStamina;
        inGameStaminaBar.value = currentStamina / maxStamina;
    }

    public float GetStamina { get { return currentStamina; } }
    public bool CanAttack {  get { return currentStamina >= attackStaminaCoast; } }
    public bool CanDefend { get { return currentStamina >= defendStaminaCoast; } }
}
