using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Handles scene transitions when the player enters an area exit trigger.
/// Fades the screen to black, stores transition data, and loads the target scene.
/// </summary>
public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private float waitToLoadTime = 1f;
    /// <summary>
    /// Called when another collider enters the trigger zone.
    /// If the player enters, begin fade and scene transition.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {// Check if the collider belongs to the Player
        if (other.gameObject.GetComponent<PlayerController>())
        {            
            // Save the name of the transition so player knows where to spawn
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);

            // Start fading to black
            UIFade.Instance.FadeToBlack();

            // Begin coroutine to load the next scene after a short delay
            StartCoroutine(LoadSceneRoutine());
        }
    }

    /// <summary>
    /// Waits a short amount of time (for fade effect),
    /// then loads the target scene.
    /// </summary>
    private IEnumerator LoadSceneRoutine()
    {
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
