using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the currently active weapon and handles attack input
// It inherits from a generic Singleton<T> class to ensure only one instance exists
public class ActiveWeapon : Singleton<ActiveWeapon>
{
    // Reference to the currently equipped weapon
    // Must implement the IWeapon interface
    public MonoBehaviour CurrentActiveWeapon { get; private set; }


    private PlayerControls playerControls;
    private float timeBetweenAttacks;


    // Tracks whether the attack button is being held down
    private bool attackButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        // Subscribes to the attack input events
        // 'started' triggers when the attack button is pressed
        playerControls.Combat.Attack.started += _ => StartAttacking();
        // 'canceled' triggers when the attack button is released
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
        AttackCooldown();

    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }
    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }
    
    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    // Handles the actual attack logic
    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
