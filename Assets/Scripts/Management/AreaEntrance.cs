using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles the spawn position of the player when entering a new scene.
/// Compares the transition name and positions the player accordingly.
/// </summary>
public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
    /// <summary>
    /// On scene load, checks if this entrance matches the transition name.
    /// If so, moves the player to this position, sets up the camera, and fades the screen in.
    /// </summary>
    private void Start()
    {
        // If this entrance matches the one set by SceneManagement, use it
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            // Move player to the spawn point (where this object is placed in the scene)
            PlayerController.Instance.transform.position = this.transform.position;

            // Reassign the camera to follow the player (necessary after scene load)
            CameraController.Instance.SetPlayerCameraFollow();

            // Fade the screen from black to clear
            UIFade.Instance.FadeToClear();
        }
    }
}
