using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls pickup behavior including animation on spawn,
/// player proximity detection, and applying the effect based on pickup type.
/// </summary>
public class Pickup : MonoBehaviour
{
    [SerializeField] private float pickUpDistance = 5f; // Distance at which pickup moves toward player
    [SerializeField] private float accelerationRate = .2f;// Speed increase as it approaches
    [SerializeField] private float moveSpeed = 3f;// Base move speed
    [SerializeField] private AnimationCurve animCurve;// Curve used to animate the spawn "pop"
    [SerializeField] private float popDuration = 1f;// Time of the pop animation
    [SerializeField] private float heightY = 1.5f;// Max height during pop
    private Vector3 moveDir;
    private Rigidbody2D rb;

    /// <summary>
    /// Enum to define what type of pickup this is.
    /// </summary>
    private enum PickUpType {
        GoldCoin,
        Stamina,
        Health,
    }
    
    [SerializeField] private PickUpType pickUpType;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        // If player is within pickup range, move toward them
        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelerationRate;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        // Apply movement toward player
        rb.velocity = moveSpeed * Time.deltaTime * moveDir;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // If player touches the pickup, apply the effect and destroy the object
        if (other.gameObject.GetComponent<PlayerController>())
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Handles the initial pop animation using an animation curve.
    /// Adds randomness to the landing position.
    /// </summary>

    private IEnumerator AnimCurveSpawnRoutine() {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            // Move toward end point while popping up based on animation curve
            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    /// <summary>
    /// Applies the effect based on the pickup type (coin, health, stamina).
    /// </summary>
    private void DetectPickupType() {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                Debug.Log("GoldCoin");
                break;
            case PickUpType.Health:
                PlayerHealth.Instance.HealPlayer();
                Debug.Log("Health");
                break;
            case PickUpType.Stamina:
                Stamina.Instance.RefreshStamina();
                Debug.Log("Stamina");
                break;
        }
    }


}
