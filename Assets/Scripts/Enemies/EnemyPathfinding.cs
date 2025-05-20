using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Speed at which the enemy moves

    private Rigidbody2D rb;        // Rigidbody component used for physics-based movement
    private Vector2 moveDir;       // Current movement direction of the enemy
    private Knockback knockback;   // Reference to Knockback component to check if enemy is knocked back
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Get required components at the start
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // If the enemy is currently being knocked back, skip normal movement
        if (knockback.GettingKnockedBack) { return; }

        // Move the enemy in the current direction at the specified speed
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    // Sets the direction in which the enemy should move.
    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
}
