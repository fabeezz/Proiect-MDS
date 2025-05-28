using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the behavior of a growing magic laser that faces the mouse
/// and increases in length over time until it hits an indestructible object.
/// </summary>
public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 2f;
    private bool isGrowing = true;
    private float laserRange;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFaceMouse();
    }

    /// <summary>
    /// Called when the laser collides with another collider.
    /// If it hits an indestructible object, stop growing.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Stop growing if laser hits a solid, indestructible object
        if (other.gameObject.GetComponent<Indestructible>() && !other.isTrigger)
        {
            isGrowing = false;
        }
    }

    /// <summary>
    /// Starts the laser growth animation toward a given range.
    /// </summary>
    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    /// <summary>
    /// Coroutine that gradually increases the laser's visual size and collider over time.
    /// Also starts a slow fade effect once growth stops.
    /// </summary>
    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;

        while (spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;

            // Calculate interpolation factor [0, 1]
            float linearT = timePassed / laserGrowTime;

            // Update visual length of the laser sprite
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            // Update collider size and offset to match sprite
            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, capsuleCollider2D.offset.y);

            yield return null;
        }

        // Start fade effect after growth ends
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    /// <summary>
    /// Rotates the laser GameObject to point in the direction of the mouse.
    /// </summary>
    private void LaserFaceMouse()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate direction and rotate laser
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
}
