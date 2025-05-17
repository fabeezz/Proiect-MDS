using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the currently active weapon and handles attack input
// It inherits from a generic Singleton<T> class to ensure only one instance exists
public class ActiveWeapon : Singleton<ActiveWeapon>
{
    // Reference to the currently equipped weapon
    // Must implement the IWeapon interface
    [SerializeField] private MonoBehaviour currentActiveWeapon;

    private PlayerControls playerControls;

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
    }

    private void Update()
    {
        Attack();
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
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
            isAttacking = true;
            (currentActiveWeapon as IWeapon).Attack();
        }
    }
}
