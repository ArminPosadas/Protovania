using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetUp : MonoBehaviour
{
    public static GameSetUp GS;
    public Transform[] spawnPoints;

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
        SceneManager.LoadScene(MultiplayerSettings.multiplayerSettings.menuScene);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
            DisconnectPlayer();
    }
}
