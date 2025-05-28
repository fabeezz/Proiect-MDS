using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin, health, stamina;

    public void DropItems() {
        int randomNum = Random.Range(1, 5);

        switch (randomNum) {
            case 1:
                Instantiate(health, transform.position, Quaternion.identity);
                break;

            case 2:
                Instantiate(stamina, transform.position, Quaternion.identity);
                break;

            case 3:
                int randomAmountOfGold = Random.Range(1, 4);
                for (int i = 0; i < randomAmountOfGold; i++) {
                    Instantiate(goldCoin, transform.position, Quaternion.identity);
                }
                break;
        }
    }

}
