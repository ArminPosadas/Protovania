using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

    public int maxHealth = 10; // Maximum health of the enemy
    public int currentHealth;
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
        //   Debug.Log(currentHealth + "EnemyLife");

        currentHealth -= damage; // Reduce health by damage amount
        if (currentHealth <= 0)
        {
            Debug.Log("se murio el y va desactivar la puerta");
            abrir.vivos1 = false;

            Dead();
        }
    }

    void Dead()
    {
        // Code to handle enemy death (e.g., play animation, drop loot, etc.)
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}

