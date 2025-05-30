using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles spawning of item pickups (health, stamina, coins) when called.
/// Typically used upon enemy death or object destruction.
/// </summary>
public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin, health, stamina;

    /// <summary>
    /// Drops a random item or group of items at the current position.
    /// The chance is determined randomly: health, stamina, or multiple coins.
    /// </summary>
    public void DropItems() {
        int randomNum = Random.Range(1, 5); // Generates a number between 1 and 4

        switch (randomNum) {
            case 1:
                // Spawn one health pickup
                Instantiate(health, transform.position, Quaternion.identity);
                break;

            case 2:
                // Spawn one stamina pickup
                Instantiate(stamina, transform.position, Quaternion.identity);
                break;

            case 3:
                // Spawn 1 to 3 coins
                int randomAmountOfGold = Random.Range(1, 4);
                for (int i = 0; i < randomAmountOfGold; i++) {
                    Instantiate(goldCoin, transform.position, Quaternion.identity);
                }
                break;
                // case 4 is implicitly a "do nothing" case
        }
    }

}
