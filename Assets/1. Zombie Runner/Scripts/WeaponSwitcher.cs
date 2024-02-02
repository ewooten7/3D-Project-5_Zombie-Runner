using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        // Set the initial active weapon
        SetWeaponActive();
    }

    void Update()
    {
        // Store the previous weapon index for comparison
        int previousWeapon = currentWeapon;

        // Check for key input or mouse scroll wheel input to switch weapons
        ProcessKeyInput();
        ProcessScrollWheel();

        // If the current weapon index has changed, update the active weapon
        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessScrollWheel()
    {
        // Check if the mouse scroll wheel has been scrolled down
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // If the current weapon is the last one, wrap around to the first weapon
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                // Otherwise, switch to the next weapon
                currentWeapon++;
            }
        }

        // Check if the mouse scroll wheel has been scrolled up
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // If the current weapon is the first one, wrap around to the last weapon
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                // Otherwise, switch to the previous weapon
                currentWeapon--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        // Check for number key inputs (1, 2, 3) to switch to specific weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0; // Switch to the first weapon (index 0)
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1; // Switch to the second weapon (index 1)
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2; // Switch to the third weapon (index 2)
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        // Loop through all child objects (weapons) of the switcher
        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                // Activate the current weapon
                weapon.gameObject.SetActive(true);
            }
            else
            {
                // Deactivate other weapons
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}
