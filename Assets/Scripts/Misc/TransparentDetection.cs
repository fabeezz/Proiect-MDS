using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Fades a SpriteRenderer or Tilemap to a target transparency when the player enters the area,
/// and restores full opacity when the player exits. Useful for foreground decoration fading.
/// </summary>
public class NewBehaviourScript : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float transparencyAmount = 0.8f;

    [SerializeField] private float fadeTime = .4f;
    
    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    /// <summary>
    /// Gets references to the SpriteRenderer or Tilemap components on this GameObject.
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }
    /// <summary>
    /// When the player enters the trigger, fades the object to a lower alpha (transparency).
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
            } else if (tilemap)
            {
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }
    /// <summary>
    /// When the player exits the trigger, restores full opacity.
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
            } else if (tilemap)
            {
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }
    }
    /// <summary>
    /// Coroutine that fades a SpriteRenderer from one alpha to another over time.
    /// </summary>
    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue,
        float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }
    /// <summary>
    /// Coroutine that fades a Tilemap from one alpha to another over time.
    /// </summary>
    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue,
        float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
