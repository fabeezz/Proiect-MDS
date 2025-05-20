using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the damage logic when a projectile or hitbox collides with an enemy.
/// Damage is retrieved from the currently active weapon at runtime.
/// </summary>
public class DamageSource : MonoBehaviour
{
    // The amount of damage this source will deal upon collision
    private int damageAmount;

    private void Start()
    {
        MonoBehaviour currenActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;

        // Cast to IWeapon to access weapon info and retrieve damage
        damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }

    /// <summary>
    /// Called when this collider enters a trigger collider.
    /// Deals damage to the enemy if it has an EnemyHealth component.
    /// </summary>
    /// <param name="other">The collider this object collided with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Attempt to get the EnemyHealth component from the collided object
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

        // If the component exists, apply damage
        enemyHealth?.TakeDamage(damageAmount);
    }
}
