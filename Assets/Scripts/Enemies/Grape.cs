using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a ranged enemy (Grape) that triggers an animation and spawns a projectile.
/// Implements IEnemy to follow a consistent enemy behavior structure.
/// </summary>
public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjecilePrefab;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    
    /// <summary>
    /// Caches component references on Awake.
    /// </summary>
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Triggers the attack animation and flips the sprite to face the player.
    /// </summary>
    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);
        // Flip sprite depending on player's relative position
        if (transform.position.x - PlayerController.Instance.transform.position.x < 0)
        {
            spriteRenderer.flipX = false; // Player is to the right
        }
        else
        {
            spriteRenderer.flipX = true; // Player is to the left
        }
    }
    /// <summary>
    /// Called from animation event to spawn the projectile.
    /// </summary>
    public void SpawnProjectileAnimEvent()
    {
        Instantiate(grapeProjecilePrefab, transform.position, Quaternion.identity);
    }
}
