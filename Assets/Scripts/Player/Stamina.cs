using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's stamina system, including usage, regeneration over time,
/// and updating the stamina UI. Implements a Singleton pattern for global access.
/// </summary>
public class Stamina : Singleton<Stamina>
{
    /// <summary>
    /// Current stamina value. Cannot be modified externally.
    /// </summary>
    public int currentStamina { get; private set; }

    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;
    private const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    /// <summary>
    /// Sets up the Singleton instance and initializes stamina values.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        currentStamina = startingStamina;
    }

    /// <summary>
    /// Finds the UI container that holds the stamina indicators.
    /// </summary>
    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    /// <summary>
    /// Decreases stamina by 1 and updates the UI.
    /// </summary>
    public void UseStamina()
    {
        currentStamina--;
        UpdateStaminaImages();
    }

    /// <summary>
    /// Increases stamina by 1 if not at maximum, and updates the UI.
    /// </summary>
    public void RefreshStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina++;
        }

        UpdateStaminaImages();
    }

    /// <summary>
    /// Coroutine that refreshes stamina periodically over time.
    /// </summary>
    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    /// <summary>
    /// Updates the stamina UI images to reflect the current stamina state.
    /// Also starts the refresh coroutine if stamina is not full.
    /// </summary>
    private void UpdateStaminaImages()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            if (i <= currentStamina - 1)
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            }
            else
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
            }
        }

        if (currentStamina < maxStamina)
        {
            StopAllCoroutines();// reset any running coroutines
            StartCoroutine(RefreshStaminaRoutine());
        }
    }

}
