using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a magic staff weapon that can attack by firing a laser.
/// Follows the mouse cursor and plays an attack animation when triggered.
/// </summary>
public class Staff : MonoBehaviour, IWeapon
{
    // Holds data related to the weapon (e.g., name, damage, cooldown)
    [SerializeField] private WeaponInfo weaponInfo;

    // The laser projectile prefab that will be instantiated when attacking
    [SerializeField] private GameObject magicLaser;

    // The position in the scene where the laser projectile will spawn from
    [SerializeField] private Transform magicLaserSpawnPoint;

    // Reference to the Animator component attached to this GameObject
    private Animator myAnimator;

    // Cached hash for the "Attack" animation trigger to improve performance
    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    /// <summary>
    /// Called when the object is initialized (before Start).
    /// Sets up the animator reference.
    /// </summary>
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Called once per frame.
    /// Makes the staff follow the mouse position with rotation.
    /// </summary>
    private void Update()
    {
        MouseFollowWithOffset();
    }

    /// <summary>
    /// Triggers the attack animation for the staff.
    /// </summary>
    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);
    }

    /// <summary>
    /// Called via animation event to instantiate the laser projectile.
    /// </summary>
    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    /// <summary>
    /// Returns the information related to this weapon.
    /// </summary>
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    /// <summary>
    /// Rotates the staff to follow the mouse position on screen,
    /// flipping on the Y axis if the mouse is to the left of the player.
    /// </summary>
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;

        // Converts the player's world position to screen position
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        // Calculates the angle between mouse and origin
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            // Flip the staff on the Y-axis if mouse is left of player
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            // Normal rotation if mouse is right of player
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
