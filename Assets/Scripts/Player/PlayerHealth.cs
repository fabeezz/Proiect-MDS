using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the player's health, damage, healing, and death logic.
/// Implements the Singleton pattern for global access, and interfaces with 
/// knockback, flash effects, screen shake, and UI updates.
/// </summary>
public class PlayerHealth : Singleton<PlayerHealth>
{
    /// <summary>
    /// Indicates whether the player is dead.
    /// </summary>
    public bool isDead { get; private set; }
    
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    // Cached references
    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;
    
    const string HEALTH_SLIDER_TEXT = "Health Slider"; 
    const string TOWN_TEXT = "Scene1";
    // Animator trigger hash for death animation
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    /// <summary>
    /// Initializes the Singleton instance and caches component references.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }
    /// <summary>
    /// Initializes health values and updates the health UI slider.
    /// </summary>
    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;

        UpdateHealthSlider();
    }
    /// <summary>
    /// Continuously checks for collisions with enemies to apply damage.
    /// </summary>
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();


        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }
    /// <summary>
    /// Heals the player by one unit if not already at maximum health.
    /// Updates the health slider accordingly.
    /// </summary>
    public void HealPlayer()
    {
        if (currentHealth < maxHealth) {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }
    /// <summary>
    /// Applies damage to the player and triggers knockback, flash effect, 
    /// and screen shake. If health drops to zero, initiates death sequence.
    /// </summary>
    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        
        Debug.Log("Player took damage");

        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }
    /// <summary>
    /// Updates the UI slider to reflect the current health.
    /// Finds the slider in the scene if it hasn't been cached yet.
    /// </summary>
    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();

        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    /// <summary>
    /// Checks whether the player's health is zero or below, 
    /// then handles death logic including animation, destruction, and scene reload.
    /// </summary>
    private void CheckIfPlayerDeath()
    {
        if(currentHealth <= 0 && !isDead){
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }
    /// <summary>
    /// Coroutine that waits for a short delay after death, 
    /// then destroys the player object and loads a specified scene.
    /// </summary>
    private IEnumerator DeathLoadSceneRoutine() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }

    /// <summary>
    /// Coroutine that handles the damage recovery period,
    /// during which the player is invulnerable to further damage.
    /// </summary>
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
