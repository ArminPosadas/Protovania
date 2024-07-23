using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Jobs;
using Unity.VisualScripting;

public class Gun : MonoBehaviourPunCallbacks
{
    public Transform gunTransform;
    public Transform bullet;
    public float bulletSpeed = 10;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    void Start()
    {
        if (bullet == null)
        {
            Debug.LogError("no es un prefab la bala");
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
            Debug.LogError("la bala o el transform no esta");
            return;
        }

        Transform bulletInstance = Instantiate(bullet, gunTransform.position, gunTransform.rotation);
        if (bulletInstance == null)
        {
            Debug.LogError("fallo la bala");
            return;
        }

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = gunTransform.forward * bulletSpeed;
            Debug.Log("se dispara con una velocidad: " + rb.velocity);
        }
        else
        {
            Debug.LogError("no hay bullet prefab");
        }
    }
}


