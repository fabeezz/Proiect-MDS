using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls the sword attack mechanics, animations and hit detection
/// </summary>
public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab; // Prefab for the slash animation effect
    [SerializeField] private Transform slashAnimSpawnPoint; // Spawn point for the slash animation
    [SerializeField] private Transform weaponCollider; // Collider used for sword hits
    [SerializeField] private float swordAttackCD = .5f; // Cooldown between sword attacks

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private bool attackButtonDown, isAttacking = false;

    private GameObject slashAnim;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {// Subscribe to attack input events
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

    }

    private void Update()
    {// Update sword rotation based on mouse position
        MouseFollowWithOffset();
        Attack(); // Handle attack input and attack logic
    }
    private void StartAttacking()
    {
        attackButtonDown = true;  // Called when the attack button is pressed
    }

    private void StopAttacking()
    {
        attackButtonDown = false;  // Called when the attack button is released
    }

    private void Attack()
    {// Start an attack if attack button is down and not already attacking
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            // Spawn the slash animation
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        }
    }

    private IEnumerator AttackCDRoutine()
    {// Cooldown after attacking before being able to attack again
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
    }

    public void DoneAttackingAnimEvent()
    {// Called via animation event to disable the weapon collider
        weaponCollider.gameObject.SetActive(false);
    }

    
    public void SwingUpFlipAnimEvent()
    {// Animation event for swinging upwards and flipping the slash animation
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    { // Animation event for swinging downwards and setting normal rotation
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    { // Makes the sword follow the mouse position with correct rotation
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);

        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }
}
