using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Handles UI fading transitions using a fullscreen Image overlay.
/// Can fade to black or fade to clear. Uses a Singleton pattern for global access.
/// </summary>
public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 1f;

    private IEnumerator fadeRoutine;

    /// <summary>
    /// Fades the screen to black.
    /// Useful for scene transitions or death animations.
    /// </summary>
    public void FadeToBlack()
    {
        Debug.Log("Fading to black");
        // Stop any ongoing fade before starting a new one
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(1);// 1 = fully opaque
        StartCoroutine(fadeRoutine);
    }

    /// <summary>
    /// Fades the screen from black to clear (transparent).
    /// Useful when entering gameplay from a transition.
    /// </summary>
    public void FadeToClear()
    {
        if (fadeRoutine != null)
        {
            Debug.Log("Fading to clear");
            StopCoroutine(fadeRoutine);
        }

        // Ensure the alpha is reset to 0
        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 1);
        fadeRoutine = FadeRoutine(0);// 0 = fully transparent
        StartCoroutine(fadeRoutine);
    }
    /// <summary>
    /// Coroutine that interpolates the screen fade alpha toward the target value.
    /// </summary>
    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}
