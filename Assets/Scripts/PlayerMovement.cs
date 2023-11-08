using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : NetworkBehaviour
{
    [Header("SPEED Values")]
    [SerializeField] float baseSpeed = 5f;
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float defendSpeed = 2.5f;
    [SerializeField] float stunSpeed = 3f;
    [SerializeField] float turnSpeed = 6f;

    [Header("TIME Values")]
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float stunDuration = 1f;

    [Header("COMBAT Values")]
    [SerializeField] float damage = 10f;
    [SerializeField] float defend = 5f;

    [Header("FX's")]
    [SerializeField] ParticleSystem dashFX;
    [SerializeField] ParticleSystem ultiFX;

    bool isDefending;
    float moveSpeed;
    Vector3 moveInput;
    Rigidbody rb;
    ManaSystem manaSystem;
    StaminaSystem staminaSystem;
    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        manaSystem = GetComponent<ManaSystem>();
        staminaSystem = GetComponent<StaminaSystem>();
        moveSpeed = baseSpeed;
    }

    void FixedUpdate()
    {
        if (!IsOwner) { return; }
        Run();
        Turn();
    }
    private void Update()
    {
        if (!IsOwner) { return; }
        DoDash();
        Attack();
        Defend();
        DoUlti();
    }

    void OnMove(InputValue value)
    {
        if (!IsOwner) { return; }
        moveInput.x = value.Get<Vector2>().x;
        moveInput.z = value.Get<Vector2>().y;
        if (moveInput != Vector3.zero)
        {
            if (!isDefending)
            {
                animator.SetBool(name = "isMoving", true);
                animator.SetBool(name = "isDefending", false);
            }
            else
            {
                animator.SetBool(name = "isMoving", false);
                animator.SetBool(name = "isDefending", true);
            }    
        }
        else
        {
            animator.SetBool(name = "isMoving", false);
            animator.SetBool(name = "isDefending", false);
        }
    }

    void Run()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    void Turn()
    {
        if(moveInput == Vector3.zero) { return; }
        float _angle = Mathf.Atan2(-moveInput.z, moveInput.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, _angle, 0), Time.deltaTime * turnSpeed);
        //Debug.Log(_angle);
    }

    void DoDash()
    {
        if (manaSystem.CanDash == false) { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashFX.Play();
            animator.SetTrigger(name = "doDash");
            manaSystem.SpendDashMana();
            StartCoroutine(StartDashForDuration());
        }
    }

    IEnumerator StartDashForDuration()
    {
        float _timer = 0;

        while (_timer <= dashDuration) 
        {
            moveSpeed = dashSpeed;
            _timer += Time.deltaTime;
            yield return null;
        }
        moveSpeed = baseSpeed;
    }

    public void ImHit()
    {
        StartCoroutine(StartStunForDuration());
    }

    IEnumerator StartStunForDuration()
    {
        float _timer = 0;

        while (_timer <= stunDuration)
        {
            moveSpeed = stunSpeed;
            _timer += Time.deltaTime;
            yield return null;
        }
        moveSpeed = baseSpeed;
    }

    void Defend()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            staminaSystem.SpendDefendStamina();
        }
        if (Input.GetMouseButton(1))
        {
            isDefending = true;
            moveSpeed = defendSpeed;
        }
        else
        {
            isDefending = false;
            moveSpeed = baseSpeed;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            staminaSystem.SpendAttackStamina();
            animator.SetTrigger("doAttack");
        }
    }

    public float GetDamage { get { return damage; } }
    public bool IsDefending { get {  return isDefending; } } // to HealthSystem.cs
    public float GetDefend {  get { return defend; } } // same above.

    void DoUlti()
    {
        if (!manaSystem.CanCastUlti) {  return; }
        if (Input.GetMouseButtonDown(2))
        {
            ultiFX.Play();
            manaSystem.SpendDashMana();
        }
    }
}
