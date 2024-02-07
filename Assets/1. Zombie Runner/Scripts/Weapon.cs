using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // References to various components and variables
    [SerializeField] Camera FPCamera;              // Reference to the first-person camera
    [SerializeField] float range = 100f;          // Range of the weapon
    [SerializeField] float damage = 30f;          // Damage dealt by the weapon
    [SerializeField] ParticleSystem muzzleFlash;  // Muzzle flash effect
    [SerializeField] GameObject hitEffect;        // Impact effect when a shot hits
    [SerializeField] Ammo ammoSlot;               // Reference to the ammo inventory
    [SerializeField] AmmoType ammoType;           // Type of ammo used by this weapon
    [SerializeField] float timeBetweenShots = 0.5f; // Cooldown time between shots
    [SerializeField] TextMeshProUGUI ammoText;    // UI text for displaying ammo count
    [SerializeField] private AudioSource shootingAudioSource; // Reference to AudioSource

    bool canShoot = true; // Flag to control whether the weapon can shoot

    private void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo(); // Update and display the remaining ammo count
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
            {
                PlayShootingSound(); // Play the shooting sound effect if there is enough ammo
                StartCoroutine(Shoot()); // Start shooting when left mouse button is pressed
            }
            else
            {
                // Handle case when weapon is out of ammo (optional)
                // You may want to play a click sound or display a message indicating out of ammo
            }
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString(); // Display the current ammo count in the UI
    }

    IEnumerator Shoot()
    {
        canShoot = false; // Disable shooting temporarily
        PlayMuzzleFlash(); // Play the muzzle flash effect
        ProcessRaycast();  // Handle shooting and hit detection
        ammoSlot.ReduceCurrentAmmo(ammoType); // Decrease ammo count
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true; // Enable shooting after cooldown
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play(); // Play the muzzle flash particle effect
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit); // Create impact effect at the hit point
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return; // Check if the hit object has an EnemyHealth component
            target.TakeDamage(damage); // Deal damage to the enemy
        }
        else
        {
            return; // No hit detected, return without doing anything
        }
    }

    private void PlayShootingSound()
    {
        if (shootingAudioSource != null)
        {
            shootingAudioSource.Play();
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        // Instantiate and destroy the impact effect at the hit point
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }
}
