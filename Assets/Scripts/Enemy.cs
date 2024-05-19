using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10; // Maximum health of the enemy
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by damage amount
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        // Code to handle enemy death (e.g., play animation, drop loot, etc.)
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}