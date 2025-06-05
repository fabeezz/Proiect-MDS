using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tracks the number of enemies killed during the game.
/// Listens to the EnemyHealth.OnEnemyKilled event and logs kill count in the console.
/// Demonstrates the Observer Pattern: this class acts as an observer to EnemyHealth.
/// </summary>
public class KillCounter : MonoBehaviour
{
    // Internal counter to track how many enemies were defeated
    private int enemyKills = 0;

    /// <summary>
    /// Subscribes to the OnEnemyKilled event when this object becomes active in the scene.
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("KillCounter activated.");
        // Subscribe to the static event from EnemyHealth
        EnemyHealth.OnEnemyKilled += IncrementKillCount;
    }

    /// <summary>
    /// Unsubscribes from the event when the object is disabled or destroyed,
    /// to prevent memory leaks or null references.
    /// </summary>
    private void OnDisable()
    {
        Debug.Log("KillCounter deactivated.");
        // Unsubscribe from the event to stay safe
        EnemyHealth.OnEnemyKilled -= IncrementKillCount;
    }

    /// <summary>
    /// Called automatically whenever an enemy dies and the event is raised.
    /// Increments the internal kill counter and logs the result in the Unity console.
    /// </summary>
    /// <param name="enemy">The enemy that was killed (optional, not used here)</param>
    private void IncrementKillCount(EnemyHealth enemy)
    {
        enemyKills++;  // Increase kill count
        Debug.Log($"[KILL] Enemy killed. Total kills: {enemyKills}");  // Output to console
    }
}
