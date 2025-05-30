using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Starts an idle animation from a random time offset, to add variety among identical characters.
/// </summary>
public class RandomIdleAnimation : MonoBehaviour
{
    private Animator myAnimator;

    /// <summary>
    /// Caches the Animator component attached to the GameObject.
    /// </summary>
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    /// <summary>
    /// On start, plays the current animation from a random time offset (between 0 and 1).
    /// This avoids all characters starting their animations at the same frame.
    /// </summary>
    private void Start()
    {
        if (!myAnimator) { return; }
        // Get current animation state from the default layer (layer 0)
        AnimatorStateInfo state = myAnimator.GetCurrentAnimatorStateInfo(0);
        // Play the same animation, but from a random normalized time (0 = start, 1 = end)
        myAnimator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
