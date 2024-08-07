using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Engineer : FPSMovement
{
    public float buffDuration = 30f;
    public float fireRateIncrease = 0.05f;
    public GameObject buffAreaPrefab; // Prefab que representa la zona del buff

    private bool isBuffActive = false;
    private float buffEndTime;
    private GameObject buffAreaInstance;

    void Update()
    {
        base.Update();

        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                RPC_ToggleBuff();
            }
            //desactiva el buff pasando el tiempo
            if (isBuffActive && Time.time > buffEndTime)
            {
                RPC_DeactivateBuff();
            }
        }
    }

    void RPC_ToggleBuff()
    {
        if (isBuffActive)
        {
            RPC_DeactivateBuff();
        }
        else
        {
            RPC_ActivateBuff();
        }
    }
    //activa el buff y genera la zona en el mapa
    [PunRPC]
    void RPC_ActivateBuff()
    {
        isBuffActive = true;
        buffEndTime = Time.time + buffDuration;

        if (buffAreaPrefab != null)
        {
            buffAreaInstance = Instantiate(buffAreaPrefab, transform.position, Quaternion.identity);
            buffAreaInstance.transform.localScale = new Vector3(buffDuration * 2, 0.1f, buffDuration * 2);
        }

        Debug.Log("Buff activado.");
    }
    //desactiva el buff y destruye la zona en el mapa
    void RPC_DeactivateBuff()
    {
        isBuffActive = false;

        if (buffAreaInstance != null)
        {
            Destroy(buffAreaInstance);
        }

        Debug.Log("Buff desactivado.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isBuffActive) return;

        // Verifica si el objeto que entro en la zona es un aliado usando la etiqueta
        if (other.CompareTag("Player"))
        {
            // Aplica el buff al aliado
            Gun allyGun = other.GetComponent<Gun>();
            if (allyGun != null)
            {
                StartCoroutine(ApplyBuff(allyGun));
                Debug.Log("Buff aplicado a: " + other.name);
            }
        }
    }
    //corutina para aplicar el buff temporalmente
    private IEnumerator ApplyBuff(Gun allyGun)
    {
        allyGun.fireRate -= fireRateIncrease;
        yield return new WaitForSeconds(buffDuration);
        allyGun.fireRate += fireRateIncrease;
        Debug.Log("Buff expirado para: " + allyGun.gameObject.name);
    }
}
