using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Melee : FPSMovement
{
    private bool isBoosting = false;
    private float boostDuration = 1f;
    private float boostStartTime;
    private float originalSpeed;
    private Vector3 originalDirection;
    private float playerRadius = 1f; // Adjust based on your player's size

    void Update()
    {
        base.Update();

        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isBoosting)
            {
                photonView.RPC("RPC_StartBoost", RpcTarget.All);
            }

            if (isBoosting)
            {
                float elapsed = Time.time - boostStartTime;
                if (elapsed < boostDuration)
                {
                    BoostPlayer();
                }
                else
                {
                    photonView.RPC("RPC_StopBoost", RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    void RPC_StartBoost()
    {
        isBoosting = true;
        originalSpeed = movementSpeed;
        movementSpeed *= 2f;
        originalDirection = playerCamera.transform.forward;
        boostStartTime = Time.time;
    }

    void BoostPlayer()
    {
        Vector3 moveDirection = new Vector3(originalDirection.x, 0f, originalDirection.z).normalized;
        myCC.Move(moveDirection * movementSpeed * Time.deltaTime);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Destructable"))
            {
                Debug.Log("Destroyed: " + hitCollider.gameObject.name);
                PhotonView hitPhotonView = hitCollider.gameObject.GetComponent<PhotonView>();
                if (hitPhotonView != null)
                {
                    photonView.RPC("RPC_DestroyObject", RpcTarget.All, hitPhotonView.ViewID);
                }
                else
                {
                    Debug.LogWarning("Destructable object does not have a PhotonView: " + hitCollider.gameObject.name);
                }
            }
        }
    }

    [PunRPC]
    void RPC_StopBoost()
    {
        isBoosting = false;
        movementSpeed = originalSpeed;
    }

    [PunRPC]
    void RPC_DestroyObject(int viewID)
    {
        PhotonView objPhotonView = PhotonView.Find(viewID);
        if (objPhotonView != null)
        {
            GameObject obj = objPhotonView.gameObject;
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        else
        {
            Debug.LogWarning("PhotonView not found for viewID: " + viewID);
        }
    }
}



