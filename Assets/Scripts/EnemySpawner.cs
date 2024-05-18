using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a spawnear
    public Transform[] spawnPoints; // Puntos de spawn para los enemigos
    public Transform player; // Referencia al transform del jugador


    private bool hasSpawned = false;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        if (hasSpawned) return;

        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        hasSpawned = true;
    }

}
