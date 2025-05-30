using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the object to always face the mouse cursor.
/// Useful for aiming weapons or pointing characters.
/// </summary>
public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        FaceMouse();
    }

    /// <summary>
    /// Calculates the direction from the object to the mouse position
    /// and rotates the object to face that direction.
    /// </summary>
    private void FaceMouse()
    {
        // Get mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;
        // Convert to world space coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate direction from object to mouse
        Vector2 direction = transform.position - mousePosition;
        // Rotate object to face the mouse (invert direction to point properly)
        transform.right = -direction;
    }
}
