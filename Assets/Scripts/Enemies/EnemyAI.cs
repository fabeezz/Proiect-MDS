using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 3f; // Cât de des schimbă direcția
    [SerializeField] private float roamingRange = 7f; // Cât de departe poate să se miște de punctul de start

    private enum State
    {
        Roaming
    }

    private Vector2 moveDirection;
    private Vector2 startPosition;
    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;  
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (true)
        {
            PickNewDirection();
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
    }

    private void PickNewDirection()
    {
        // Alege o direcție random
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Verifică dacă pleacă prea departe
        Vector2 potentialPosition = (Vector2)transform.position + moveDirection;
        if (Vector2.Distance(startPosition, potentialPosition) > roamingRange)
        {
            // Dacă da, inversează direcția
            moveDirection = (startPosition - (Vector2)transform.position).normalized;
        }

        enemyPathfinding.MoveTo(moveDirection);
    }


}
