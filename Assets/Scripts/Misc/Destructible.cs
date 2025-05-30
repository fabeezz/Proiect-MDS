using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows a GameObject to be destroyed when hit by a valid damage source.
/// Triggers visual effects and spawns pickup items upon destruction.
/// </summary>
public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    /// <summary>
    /// Checks for collision with a damage source or projectile.
    /// If detected, spawns pickups, plays VFX, and destroys the object.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered is a valid damage source
        if (other.gameObject.GetComponent<DamageSource>() || other.gameObject.GetComponent<Projectile>())
        {
            // Drop items if the object has a PickUpSpawner
            GetComponent<PickUpSpawner>().DropItems();
            // Instantiate destruction visual effect
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            // Destroy the object
            Destroy(gameObject);
        }
    }
}
