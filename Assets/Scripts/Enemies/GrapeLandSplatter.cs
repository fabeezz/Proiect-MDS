using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the behavior of a splatter effect left by a projectile upon landing.
/// Fades out over time and damages the player on contact shortly after spawn.
/// </summary>
public class GrapeLandSplatter : MonoBehaviour
{
    private SpriteFade spriteFade;

    /// <summary>
    /// Caches the SpriteFade component used for visual fade-out.
    /// </summary>
    private void Awake() {
        spriteFade = GetComponent<SpriteFade>();
    }
    /// <summary>
    /// Starts the fade-out effect and disables the collider after a short delay
    /// to prevent multiple hits or future collisions.
    /// </summary>
    private void Start() {
        // Begin fading the sprite visually
        StartCoroutine(spriteFade.SlowFadeRoutine());
        // Prevent lingering hitbox after 0.2 seconds
        Invoke("DisableCollider", 0.2f);
    }

    /// <summary>
    /// Detects collision with the player and applies damage.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1, transform);
    }

    /// <summary>
    /// Disables the collider to prevent further interactions.
    /// </summary>
    private void DisableCollider() {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}