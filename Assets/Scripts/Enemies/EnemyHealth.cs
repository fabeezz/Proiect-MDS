using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the health logic for an enemy character, including taking damage,
/// visual feedback, knockback, death, and item drops.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;
    public static event Action<EnemyHealth> OnEnemyKilled;



    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    /// <summary>
    /// Initializes references to components.
    /// </summary>
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }
    /// <summary>
    /// Sets the current health to the starting value.
    /// </summary>
    private void Start()
    {
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Applies damage to the enemy, triggers knockback and flash effect,
    /// and checks if the enemy should die.
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Apply knockback away from the player
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        // Flash effect for hit feedback
        StartCoroutine(flash.FlashRoutine());
        // Check for death after flash ends
        StartCoroutine(CheckDetectDeathRoutine());

    }
    /// <summary>
    /// Waits for the flash effect duration before checking for death.
    /// </summary>
    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    /// <summary>
    /// Destroys the enemy if health is depleted, plays death VFX,
    /// and spawns pickups if available.
    /// </summary>
    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickUpSpawner>().DropItems();

            OnEnemyKilled?.Invoke(this);  // <-- notifică observatorii

            Destroy(gameObject);
        }
    }
}
