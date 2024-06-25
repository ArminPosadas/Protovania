using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun; // Importa Photon

public class ChangeScene : MonoBehaviourPunCallbacks // Hereda de MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Verifica si el jugador que colisiona es el jugador local
            if (PhotonNetwork.IsMasterClient)
            {
                // Utiliza PhotonNetwork.LoadLevel para cambiar y sincronizar la escena
                PhotonNetwork.LoadLevel("MeleeTest");
            }
        }
    }
}
