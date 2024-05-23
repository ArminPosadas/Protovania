using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    public int damage = 10; // Daño del proyectil
    public float lifetime = 5f; // Tiempo de vida del proyectil antes de destruirse

    void Start()
    {
        // Destruir el proyectil después de un tiempo para evitar acumulación
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Causar daño al jugador
            Mage playerData = other.GetComponent<Mage>();
            if (playerData != null)
            {
                playerData.health -= damage;

                // Verificar si el jugador ha muerto
                if (playerData.health <= 0)
                {
                    Debug.Log("the player is dead.");
                }

                // Destruir el proyectil después de causar daño
                Destroy(gameObject);
            }
        }
        else
        {
            // Destruir el proyectil si impacta con cualquier otra cosa
            Destroy(gameObject);
        }
    }

}
