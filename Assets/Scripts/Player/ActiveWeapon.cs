using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the currently equipped weapon and controls attack input,
/// ensuring attacks occur with respect to cooldowns.
/// Inherits from a Singleton to ensure a single global instance.
/// </summary>
public class ActiveWeapon : Singleton<ActiveWeapon>
{
    // Reference to the currently active weapon script (implements IWeapon)
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    // Input system reference
    private PlayerControls playerControls;

    // Cooldown time between two attacks (in seconds)
    private float timeBetweenAttacks;

    // Tracks input state and cooldown logic
    private bool attackButtonDown, isAttacking = false;

    /// <summary>
    /// Called before Start, used to initialize references and singleton base.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        // Initialize input system
        playerControls = new PlayerControls();
    }

    /// <summary>
    /// Called when the object becomes enabled and active.
    /// Enables the input controls.
    /// </summary>
    private void OnEnable()
    {
        playerControls.Enable();
    }

    /// <summary>
    /// Called on the first frame.
    /// Sets up event listeners for attack input and initializes cooldown.
    /// </summary>
    private void Start()
    {
        // Register event for attack button press
        playerControls.Combat.Attack.started += _ => StartAttacking();

        // Register event for attack button release
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        // Initialize cooldown state
        AttackCooldown();
    }

    /// <summary>
    /// Called once per frame.
    /// Handles attack input processing.
    /// </summary>
    private void Update()
    {
        Attack();
    }

    /// <summary>
    /// Assigns a new active weapon and updates the cooldown timer based on it.
    /// </summary>
    /// <param name="newWeapon">The weapon to equip</param>
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;

        AttackCooldown();

        // Get the weapon's cooldown from its WeaponInfo
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    /// <summary>
    /// Clears the current active weapon reference.
    /// </summary>
    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    /// <summary>
    /// Starts the cooldown period after an attack.
    /// </summary>
    private void AttackCooldown()
    {
        isAttacking = true;

        // Stop any previous cooldown coroutines
        StopAllCoroutines();

        // Start a new cooldown coroutine
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    /// <summary>
    /// Coroutine that waits for the cooldown time before allowing the next attack.
    /// </summary>
    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    /// <summary>
    /// Triggered when the attack button is pressed.
    /// </summary>
    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    /// <summary>
    /// Triggered when the attack button is released.
    /// </summary>
    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    /// <summary>
    /// Attempts to perform an attack if input is held and cooldown is over.
    /// </summary>
    private void Attack()
    {
        // Can attack only if button is held, cooldown passed, and weapon is equipped
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
