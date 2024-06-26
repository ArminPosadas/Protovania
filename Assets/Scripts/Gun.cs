using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    //Se encarga de la velocidad de la bala
    public float fireRate = 0.5f;
    //Se encarga de la velocidad de disparo #Este solo funciona si se manipula en unity NO en el codigo.

    public float meleeRange = 2f; // Range of the melee attack
    public int meleeDamage = 50; // Damage dealt by the melee attack


    private float nextFireTime = 0f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFireTime)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) // Right mouse button for melee attack
        {
            MeleeAttack();
        }
    }

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        nextFireTime = Time.time + fireRate;
    }

    void MeleeAttack()
    {
        // Perform a sphere cast to detect enemies within the melee range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, meleeRange);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(meleeDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the melee range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}
