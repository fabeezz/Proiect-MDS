using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the behavior of the bow weapon.
/// Implements the IWeapon interface to provide attack functionality.
/// Spawns a projectile (arrow) and plays the firing animation.
/// </summary>
public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");// Animator trigger for firing

    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }


    /// <summary>
    /// Performs the bow's attack by instantiating an arrow and triggering the fire animation.
    /// </summary>
    public void Attack()
    {   // Play bow firing animation
        myAnimator.SetTrigger(FIRE_HASH);
        // Instantiate an arrow at the spawn point with the weapon's rotation
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        // Set the arrow's range based on the weapon info
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
    }

    /// <summary>
    /// Returns the WeaponInfo associated with this bow.
    /// </summary>
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
