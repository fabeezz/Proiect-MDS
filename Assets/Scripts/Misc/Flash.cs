using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the flash effect by swapping the sprite's material temporarily.
/// Typically used to indicate that the player or an enemy has taken damage.
/// </summary>
public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Caches references to the SpriteRenderer and its default material.
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }
    /// <summary>
    /// Returns the duration before the default material is restored.
    /// Useful for syncing other effects (e.g., knockback).
    /// </summary>
    public float GetRestoreMatTime()
    {
        return restoreDefaultMatTime;
    }

    /// <summary>
    /// Coroutine that performs the flash by switching to the flash material,
    /// waits a short duration, then restores the original material.
    /// </summary>
    public IEnumerator FlashRoutine()
    {
        // Apply flash material (e.g., white overlay)
        spriteRenderer.material = whiteFlashMat;
        // Wait for the specified duration
        yield return new WaitForSeconds(restoreDefaultMatTime);
        // Restore the original material
        spriteRenderer.material = defaultMat;
    }
}
