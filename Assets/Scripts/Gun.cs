using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Jobs;
using Unity.VisualScripting;

public class Gun : MonoBehaviourPunCallbacks
{
    public Transform gunTransform; // Transform que define la posición y rotación del cañón del arma
    public GameObject bullet;      // Prefab de la bala
    public float bulletSpeed = 10; // Velocidad de la bala
    public float fireRate = 0.5f;  // Tasa de disparo

    private float nextFireTime = 0f; // Tiempo para el próximo disparo

    void Start()
    {
        if (bullet == null)
        {
            Debug.LogError("No es un prefab la bala");
        }
        if (gunTransform == null)
        {
            Debug.LogError("el arma no es un prefab");
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButton(0) && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                photonView.RPC("RPC_Shoot", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void RPC_Shoot()
    {
        if (bullet == null || gunTransform == null)
        {
            Debug.LogError("La bala o el transform no esta asignado");
            return;
        }

        GameObject bulletInstance = Instantiate(bullet, gunTransform.position, gunTransform.rotation);
        if (bulletInstance == null)
        {
            Debug.LogError("fallo la instancia de la bala");
            return;
        }

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = gunTransform.forward * bulletSpeed;
            Debug.Log("bala disparada con una velocidad: " + rb.velocity);
        }
        else
        {
            Debug.LogError("el prefab de la bala no tiene un componente rigidbody");
        }
    }
}



