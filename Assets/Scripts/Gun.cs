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

    void Start()
    {
        
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if(Input.GetMouseButton(0)) 
            {
                //shoot
                photonView.RPC("RPC_Shoot", RpcTarget.All);
            }

        }
    }

    [PunRPC]
    void RPC_Shoot()
    {
        //bullet;
    }
}

