using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles the logic for toggling and highlighting the currently active inventory slot
public class ActiveInvetory : MonoBehaviour
{
    // Stores the index of the currently active inventory slot (0-based)
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        // Subscribe to the keyboard input for inventory toggling
        // When a key is pressed, call ToggleActiveSlot with the key's value
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());

        ToggleActiveHighlight(0);

    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    // Receives a numeric input (e.g., 1, 2, 3...) and updates the active slot accordingly
    private void ToggleActiveSlot(int numValue)
    {
        // Convert to 0-based index by subtracting 1
        ToggleActiveHighlight(numValue - 1);
    }

    // Visually highlights the active inventory slot and deactivates all others
    private void ToggleActiveHighlight(int indexNum)
    {
        // Update the current active index
        activeSlotIndexNum = indexNum;

        // Loop through all inventory slots (child transforms) and disable their highlight
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        // Enable the highlight for the selected slot
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {

        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
