using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonTeleporter : MonoBehaviourPun
{
    // Set the teleport position in the inspector or directly here
    public Vector3 teleportPosition;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that triggered the event has the tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Check if we are the owner of this object
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                // Teleport the player and synchronize it across the network
                other.gameObject.GetComponent<PhotonView>().RPC("TeleportPlayer", RpcTarget.All, teleportPosition);
            }
        }
    }
}