using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10; // Maximum health of the enemy
    private int currentHealth;
    private GameObject puerta;

    private destroyDoor2 abrir;
    
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        puerta = GameObject.Find("puerta2");
        abrir = puerta.GetComponent<destroyDoor2>();
    }



    [PunRPC]
    public void TakeDamage(int damage)
    {
        Debug.Log(currentHealth);
        currentHealth -= damage; // Reduce health by damage amount
        if (currentHealth <= 0)
        {
            abrir.vivos2 = false;
            Dead();
        }
    }

    void Dead()
    {
        // Code to handle enemy death (e.g., play animation, drop loot, etc.)
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}