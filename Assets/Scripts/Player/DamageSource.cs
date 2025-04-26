using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Detects collision with enemies and applies damage
/// </summary>
public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has an EnemyHealth component
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        // If found, apply damage
        enemyHealth?.TakeDamage(damageAmount);
    }
}
