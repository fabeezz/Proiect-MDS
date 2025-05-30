using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;


/// <summary>
/// Applies a parallax scrolling effect based on the camera's movement.
/// This gives a sense of depth by moving background layers at different speeds.
/// </summary>
public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.15f;
     
    private Camera cam; // Reference to the main camera
    private Vector2 startPos;// Original position of the object

    /// <summary>
    /// Difference between current camera position and starting point.
    /// Used to calculate how far to shift this object.
    /// </summary>
    private Vector2 travel => (Vector2)cam.transform.position - startPos;

    /// <summary>
    /// Get reference to the main camera.
    /// </summary>
    private void Awake()
    {
        cam = Camera.main;
    }
    /// <summary>
    /// Store the initial position of the background object.
    /// </summary>
    private void Start()
    {
        startPos = transform.position;
    }
    /// <summary>
    /// Moves the background layer proportionally to the camera's movement
    /// using the parallax offset factor.
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = startPos + travel * parallaxOffset;
    }
}
