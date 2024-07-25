using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
    public int damage = 25;
    public float fireRate;
    public Camera camera;
    [Header("VFX")]
    public GameObject hitVFX;

    private float nextfire;

    void Update()
    {
        if (nextfire < 0)
            nextfire = 0;

        if (Input.GetButton("Fire1") && nextfire <= 0)
        {
            nextfire = 1 / fireRate;
            Fire();
        }
        else
        {
            nextfire -= Time.deltaTime;
        }
    }

    void Fire()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);
            Debug.Log("Hit: " + hit.transform.name);

            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Hit an enemy!");
                Healt healtComponent = hit.transform.gameObject.GetComponent<Healt>();
                if (healtComponent != null)
                {
                    Debug.Log("Enemy hit: applying damage");
                    PhotonView photonView = hit.transform.gameObject.GetComponent<PhotonView>();
                    if (photonView != null && photonView.ViewID != 0)
                    {
                        photonView.RPC("TakeDamage", RpcTarget.All, damage);
                    }
                    else
                    {
                        Debug.LogError("PhotonView is null or has an invalid ViewID");
                    }
                }
            }
        }
    }
}
