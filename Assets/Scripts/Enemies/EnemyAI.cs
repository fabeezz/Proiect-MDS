using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles basic enemy behavior: roaming and attacking.
/// Uses IEnemy interface for modular attack implementations.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Attacking
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private State state;
    private EnemyPathfinding enemyPathfinding;

    /// <summary>
    /// Initializes references and sets initial state.
    /// </summary>
    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }
    /// <summary>
    /// Sets initial roaming position.
    /// </summary>
    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }
    /// <summary>
    /// Called every frame to manage current enemy behavior.
    /// </summary>
    private void Update()
    {
        MovementStateControl();
    }
    /// <summary>
    /// Switches between Roaming and Attacking states based on proximity to player.
    /// </summary>
    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }
    /// <summary>
    /// Handles random wandering behavior. Switches to attack if the player is in range.
    /// </summary>
    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);
        // Switch to attacking if the player is nearby
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
        // Pick a new direction after a time interval
        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }
    /// <summary>
    /// Triggers enemy attack behavior and optionally pauses movement.
    /// </summary>
    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (attackRange != 0 && canAttack)
        {

            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }
    /// <summary>
    /// Prevents enemy from attacking again until the cooldown is complete.
    /// </summary>
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    /// <summary>
    /// Generates a random 2D direction vector for roaming.
    /// </summary>
    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
