using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] AudioSource damageSound; 

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        
        
        if (damageSound != null && !damageSound.isPlaying)
        {
            damageSound.Play();
        }

        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
