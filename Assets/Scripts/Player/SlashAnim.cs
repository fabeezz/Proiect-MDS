using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the lifecycle of a slash particle animation.
/// Destroys the GameObject once the particle system finishes playing.
/// </summary>
public class SlashAnim : MonoBehaviour
{
    private ParticleSystem ps;

    /// <summary>
    /// Gets the ParticleSystem component attached to this GameObject.
    /// </summary>
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    /// <summary>
    /// Checks each frame if the particle effect is finished,
    /// and destroys the object if it is no longer alive.
    /// </summary>
    private void Update()
    {
        if(ps && !ps.IsAlive())
        {
            DestroySelf();
        }
    }
    /// <summary>
    /// Safely destroys the slash effect GameObject.
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
