using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    private bool isDead = false;

    // Reference to the AudioSource component for running sound
    private AudioSource runningSound;

    // Add a reference to the running sound AudioSource component
    void Start()
    {
        runningSound = GetComponent<AudioSource>();
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if (hitPoints <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        // Stop the running sound when the enemy dies
        if (runningSound != null)
        {
            runningSound.Stop();
        }

        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
