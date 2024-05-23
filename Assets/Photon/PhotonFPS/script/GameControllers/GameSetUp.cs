using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetUp : MonoBehaviour
{
    public static GameSetUp GS;
    public Transform[] spawnPoints;
    public string lobbySceneName = "LobbyScene"; // Nombre de la escena de la sala 0

    private void OnEnable()
    {
        if (GameSetUp.GS == null)
        {
            GameSetUp.GS = this;
        }
    }

    public void DisconnectPlayer()
    {
        Destroy(PhotonRoom.room.gameObject);
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(lobbySceneName); // Carga la escena de la sala 0
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
            DisconnectPlayer();
    }
}

