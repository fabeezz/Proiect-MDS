using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ScriptableObject that stores data for a weapon.
/// Includes the prefab, cooldown, damage, and range.
/// Can be reused across multiple characters or weapon slots.
/// </summary>
[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{

    public GameObject weaponPrefab;
    public float weaponCooldown;
    public int weaponDamage;
    public float weaponRange;
}
