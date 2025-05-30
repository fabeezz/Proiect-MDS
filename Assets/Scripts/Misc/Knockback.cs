using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles knockback behavior when the object takes damage.
/// Applies a physical impulse away from the damage source
/// and temporarily disables player control or movement logic.
/// </summary>
public class Knockback : MonoBehaviour
{
    /// <summary>
    /// Whether the object is currently being knocked back.
    /// This can be used to disable input or AI movement temporarily.
    /// </summary>
    public bool GettingKnockedBack { get; private set; }

    [SerializeField] private float knockBackTime = .2f;

    private Rigidbody2D rb;

    /// <summary>
    /// Caches the Rigidbody2D component on Awake.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Applies a knockback force away from the source of damage.
    /// Starts a coroutine to reset movement after a short delay.
    /// </summary>
    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }
    /// <summary>
    /// Coroutine that ends knockback after a short delay,
    /// stopping the Rigidbody's velocity and allowing movement again.
    /// </summary>
    private IEnumerator KnockRoutine()
    {
        // Wait for the knockback time and then reset the object's velocity
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockedBack = false;
    }
}
