using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Manages the main virtual camera and sets it to follow the player.
/// Uses Singleton pattern for easy global access.
/// </summary>
public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    /// <summary>
    /// Finds the active Cinemachine virtual camera in the scene
    /// and sets its Follow target to the player.
    /// </summary>
    public void SetPlayerCameraFollow()
    {
        // Finds the first CinemachineVirtualCamera in the scene
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        // Sets the follow target to the player so the camera moves with them
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}