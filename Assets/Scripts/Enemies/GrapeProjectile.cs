using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// Controls the behavior of a curved projectile (like a grape) that follows an arc
/// and drops a shadow along the ground. Creates a splatter on impact.
/// </summary>
public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject grapeProjectileShadow;
    [SerializeField] private GameObject splatterPrefab;

    /// <summary>
    /// Initializes the projectile's flight and its shadow tracking.
    /// </summary>
    private void Start()
    {
        // Spawn shadow slightly below projectile
        GameObject grapeShadow = Instantiate(grapeProjectileShadow, transform.position + new Vector3(0, -0.3f, 0),
            Quaternion.identity);
        // Target player position
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 grapeShadowStartPosition = grapeShadow.transform.position;

        // Start projectile arc movement
        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        // Start shadow movement
        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadowStartPosition, playerPos));
    }
    /// <summary>
    /// Moves the projectile from its start to target position in a curved arc,
    /// using an AnimationCurve to simulate a jump-like motion.
    /// </summary>
    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            // Evaluate height along the arc
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);
            // Move the projectile and apply vertical offset
            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);
            
            yield return null;
        }
        // On impact, create splatter and destroy projectile
        Instantiate(splatterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    /// <summary>
    /// Moves the shadow along a flat linear path from start to end,
    /// syncing visually with the projectile's horizontal travel.
    /// </summary>
    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            // Move shadow on X/Y plane
            grapeShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);
            yield return null;
        }
        Destroy(grapeShadow);
    }
}
