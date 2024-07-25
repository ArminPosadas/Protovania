using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class puerta : MonoBehaviour
{
    public int health = 100;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            TakeDamage(health); // Call TakeDamage with current health to destroy it
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
