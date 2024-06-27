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
                StartBoost();
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
                    StopBoost();
                }
            }
        }
    }

    void StartBoost()
    {
        isBoosting = true;
        originalSpeed = movementSpeed;
        movementSpeed *= 2f;
        originalDirection = playerCamera.transform.forward;
        boostStartTime = Time.time;

        // Notify other clients about the boost start
        photonView.RPC("RPC_StartBoost", RpcTarget.Others, boostStartTime, movementSpeed, originalDirection);
    }

    [PunRPC]
    void RPC_StartBoost(float startTime, float speed, Vector3 direction)
    {
        isBoosting = true;
        boostStartTime = startTime;
        originalSpeed = speed / 2f;  // Revert to original speed for non-owners
        movementSpeed = speed;
        originalDirection = direction;
    }

    void BoostPlayer()
    {
        Vector3 moveDirection = new Vector3(originalDirection.x, 0f, originalDirection.z).normalized;
        myCC.Move(moveDirection * movementSpeed * Time.deltaTime);

        if (photonView.IsMine)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Destructable"))
                {
                    Debug.Log("Destroyed: " + hitCollider.gameObject.name);
                    PhotonView targetPhotonView = hitCollider.GetComponent<PhotonView>();
                    if (targetPhotonView != null && targetPhotonView.IsMine)
                    {
                        PhotonNetwork.Destroy(hitCollider.gameObject);
                    }
                }
            }
        }
    }

    void StopBoost()
    {
        isBoosting = false;
        movementSpeed = originalSpeed;

        // Notify other clients about the boost stop
        photonView.RPC("RPC_StopBoost", RpcTarget.Others);
    }

    [PunRPC]
    void RPC_StopBoost()
    {
        isBoosting = false;
        movementSpeed = originalSpeed;
    }
}

