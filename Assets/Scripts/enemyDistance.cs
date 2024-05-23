using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDistance : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform shootingPoint; // Punto de disparo
    public float fireRate = 1.0f; // Intervalo entre disparos
    public float bulletSpeed = 10f; // Velocidad del proyectil
    public float detectionRange = 20f; // Rango de detección del jugador

    private Transform closestPlayer;
    private float nextFireTime = 0f;

    void Update()
    {
        FindClosestPlayer();

        if (closestPlayer != null && Vector3.Distance(transform.position, closestPlayer.position) <= detectionRange)
        {
            // Apuntar hacia el jugador más cercano
            Vector3 direction = (closestPlayer.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Disparar si es el momento
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }
    }

    void Fire()
    {
        // Instanciar el proyectil
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Calcular la dirección hacia el jugador y establecer la velocidad del proyectil
        Vector3 direction = (closestPlayer.position - shootingPoint.position).normalized;
        rb.velocity = direction * bulletSpeed;

        // Orientar el proyectil en la dirección del disparo
        bullet.transform.forward = direction;
    }

}
