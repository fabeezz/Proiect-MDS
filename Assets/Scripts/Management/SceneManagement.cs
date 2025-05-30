using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages scene transition metadata.
/// Stores the name of the transition portal that triggered the scene load.
/// </summary>
public class SceneManagement : Singleton<SceneManagement>
{
    /// <summary>
    /// Stores the name of the last scene transition used.
    /// This is checked to position the player correctly upon loading the new scene.
    /// </summary>
    public string SceneTransitionName { get; private set; }

    /// <summary>
    /// Sets the name of the transition portal used before loading the next scene.
    /// This is later used by the PlayerSpawner to determine spawn position.
    /// </summary>
    public void SetTransitionName(string sceneTransitionName)
    {
        // Playerul e repoziționat doar când numele tranziției coincide
        this.SceneTransitionName = sceneTransitionName;
    }
}