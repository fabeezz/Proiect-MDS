using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a UI slot in the inventory that holds a reference to a weapon.
/// This is used for selecting or equipping weapons from the inventory menu.
/// </summary>
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;

    /// <summary>
    /// Returns the WeaponInfo assigned to this inventory slot.
    /// </summary>
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
