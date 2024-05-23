using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    public int damage = 10; // Da�o del proyectil
    public float lifetime = 5f; // Tiempo de vida del proyectil antes de destruirse

    void Start()
    {
        // Destruir el proyectil despu�s de un tiempo para evitar acumulaci�n
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Causar da�o al jugador
            Data playerData = other.GetComponent<Data>();
            if (playerData != null)
            {
                playerData.vidaPlayer -= damage;

                // Verificar si el jugador ha muerto
                if (playerData.vidaPlayer <= 0)
                {
                    Debug.Log("the player is dead.");
                }

                // Destruir el proyectil despu�s de causar da�o
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
