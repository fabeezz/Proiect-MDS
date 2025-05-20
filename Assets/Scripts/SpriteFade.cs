using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles gradual fading of a sprite's visibility over time,
/// then destroys the GameObject after the fade is complete.
/// </summary>
public class SpriteFade : MonoBehaviour
{
    // Time in seconds it takes for the sprite to fully fade out
    [SerializeField] private float fadeTime = 0.4f;

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Coroutine that gradually fades the sprite's alpha to 0,
    /// then destroys the GameObject.
    /// </summary>
    public IEnumerator SlowFadeRoutine()
    {
        float elapsedTime = 0f;

        // Save the current alpha (transparency) value
        float startValue = spriteRenderer.color.a;

        // Gradually reduce alpha over the duration of fadeTime
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;

            // Compute new alpha using linear interpolation
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime);

            // Apply new color with updated alpha
            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                newAlpha
            );

            yield return null; // Wait until next frame
        }

        // Destroy the GameObject after fading is done
        Destroy(gameObject);
    }
}
