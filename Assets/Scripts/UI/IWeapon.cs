/// <summary>
/// Interface for all weapon types. 
/// Ensures consistency across different weapon implementations (e.g., bow, staff, laser).
/// </summary>
interface IWeapon
{
    /// <summary>
    /// Called to perform the weapon's attack action.
    /// Implementation depends on the weapon type.
    /// </summary>
    public void Attack();
    /// <summary>
    /// Returns the ScriptableObject containing this weapon's stats and properties.
    /// </summary>
    public WeaponInfo GetWeaponInfo();
}
