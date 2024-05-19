using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 20; // Damage dealt by the bullet

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
        }
        // Destroy the bullet regardless of what it collides with
        Destroy(gameObject);
    }
}
