using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    [SerializeField] AudioSource pickupSound;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);

            // Play the pickup sound effect when the player picks up ammo
            if (pickupSound != null && !pickupSound.isPlaying)
            {
                pickupSound.Play();
            }

            Destroy(gameObject);
        }
    }
}