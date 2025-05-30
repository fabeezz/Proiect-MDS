using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Manages the player's gold economy.
/// Keeps track of the current gold amount and updates the UI using TextMeshPro.
/// Implements Singleton for easy global access.
/// </summary>
public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold = 0;

    private const string COIN_AMOUNT_TEXT = "Gold Amount Text";
    /// <summary>
    /// Increases the player's gold by 1 and updates the gold UI text.
    /// </summary>
    public void UpdateCurrentGold()
    {
        currentGold += 1;
        // Lazy initialization of the UI reference if it hasn't been set
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        // Format gold as 3-digit number, e.g., 001, 045, 123
        goldText.text = currentGold.ToString("D3");
    }
}
