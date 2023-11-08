using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : NetworkBehaviour
{
    [SerializeField] float maxMana = 100f;
    [SerializeField] float manaRecoveryRate = 2;
    [SerializeField] Slider inGameManaBar;

    float dashManaCoast = 30f;
    float ultiManaCoast = 40f;

    //Slider manaBar;
    float currentMana;

    void Awake()
    {
        //manaBar = GameObject.FindGameObjectWithTag("manaBar").GetComponent<Slider>();
        currentMana = maxMana;
    }

    void FixedUpdate()
    {
        RecoverMana();
        UpdateManaBar();
    }

    public float GetMana { get { return currentMana; } }
    public bool CanDash { get { return currentMana >= dashManaCoast; } }
    public bool CanCastUlti { get { return currentMana >= ultiManaCoast; } }

    public void SpendDashMana()
    {
        currentMana -= dashManaCoast;
    }

    public void SpendUltiMana()
    {
        currentMana -= ultiManaCoast;
    }

    void RecoverMana()
    {
        if (currentMana >= maxMana) { return; }
        currentMana += manaRecoveryRate * Time.fixedDeltaTime;
    }

    void UpdateManaBar()
    {
        //manaBar.value = currentMana / maxMana;
        inGameManaBar.value = currentMana / maxMana; 
    }
}
