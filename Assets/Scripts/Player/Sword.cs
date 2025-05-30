using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles sword behavior including attack animations, hitbox activation,
/// directional flipping, and orientation toward the mouse cursor.
/// Implements the IWeapon interface.
/// </summary>
public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = .5f;
    [SerializeField] private WeaponInfo weaponInfo;


    private Transform weaponCollider;         // Reference to the active weapon's collider (used for hit detection)
    private Animator myAnimator;              // Animator attached to the sword
    private GameObject slashAnim;             // Instance of the slash animation object


    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Get reference to the weapon collider from the player controller
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        // Find the spawn point for the slash animation by name (can be dragged via Inspector too)
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }
    private void Update()
    {
        // Continuously rotate the weapon to face the mouse cursor
        MouseFollowWithOffset();
    }

    /// <summary>
    /// Returns the ScriptableObject containing the weapon's stats.
    /// </summary>
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    /// <summary>
    /// Triggers the sword attack animation, activates hitbox,
    /// and instantiates the slash visual.
    /// </summary>
    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
        // Enable the hitbox (collider) to deal damage
        weaponCollider.gameObject.SetActive(true);
        // Create slash animation effect
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    /// <summary>
    /// Animation event: called when the sword slash is finished.
    /// Disables the hitbox to prevent constant damage.
    /// </summary>
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    /// <summary>
    /// Animation event: flips slash animation when swinging upward.
    /// </summary>
    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    /// <summary>
    /// Animation event: resets slash rotation for downward swing.
    /// </summary>
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    /// <summary>
    /// Rotates the weapon to follow the mouse cursor with horizontal flip support.
    /// Adjusts both the weapon and its collider to match player orientation.
    /// </summary>
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
