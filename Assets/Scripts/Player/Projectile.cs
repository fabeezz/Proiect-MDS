using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls projectile behavior including movement, collision detection,
/// hit effects, and destruction after exceeding range.
/// Can be used for both player and enemy projectiles.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;

    private Vector3 startPosition;

    /// <summary>
    /// Saves the starting position of the projectile for range tracking.
    /// </summary>
    private void Start()
    {
        startPosition = transform.position;
    }
    /// <summary>
    /// Updates projectile each frame by moving it and checking range.
    /// </summary>
    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }
    /// <summary>
    /// Updates the range this projectile can travel.
    /// </summary>
    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }
    /// <summary>
    /// Allows dynamic update of projectile speed (optional).
    /// </summary>
    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
    /// <summary>
    /// Handles collision with enemies, player, or indestructible objects.
    /// Applies damage if applicable and spawns hit VFX.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (!other.isTrigger && (enemyHealth || indestructible || player))
        {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                // Apply damage only if projectile is from the opposite side
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (!other.isTrigger && indestructible)
            {
                // Hit a wall or static object
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    /// <summary>
    /// Destroys the projectile if it exceeds its allowed range.
    /// </summary>
    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Moves the projectile forward in the local right direction.
    /// </summary>
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
