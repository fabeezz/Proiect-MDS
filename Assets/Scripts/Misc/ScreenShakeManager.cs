using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Handles camera shake effects using Cinemachine Impulse.
/// Implements Singleton for global access by any system that needs to trigger a screen shake.
/// </summary>
public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    private CinemachineImpulseSource source;

    /// <summary>
    /// Initializes the Cinemachine impulse source component.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        source = GetComponent<CinemachineImpulseSource>();
    }
    /// <summary>
    /// Triggers a screen shake by generating a Cinemachine impulse.
    /// Can be called from anywhere via the Singleton instance.
    /// </summary>
    public void ShakeScreen()
    {
        source.GenerateImpulse();
    }
}
