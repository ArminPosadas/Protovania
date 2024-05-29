/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNetwork : MonoBehaviourPunCallbacks
{
    [SerializeField] private string level;
    public static PlayerNetwork Instance;
    private string playerName;
    private PhotonView photonView;
    private int playersInGame = 0;
    private ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private PlayerMovement currentPlayer;
    private Coroutine pingCoroutine;

    private void Awake()
    {
        Instance = this;
        photonView = GetComponent<PhotonView>();

        playerName = "Player#" + Random.Range(1000, 9999);

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == level)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
    }

    private void MasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
        photonView.RPC("RPC_LoadGameOthers", RpcTarget.Others);
    }

    private void NonMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(level);
    }

    [PunRPC]
    private void RPC_LoadedGameScene(Player photonPlayer)
    {
        Debug.Log("RPC_LoadedGameScene");
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            Debug.Log("All players are in the game scene");
            photonView.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    public void NewHealth(Player photonPlayer, int health)
    {
        photonView.RPC("RPC_NewHealth", photonPlayer, health);
    }

    [PunRPC]
    private void RPC_NewHealth(int health)
    {
        if (currentPlayer == null)
        {
            return;
        }
        if (health <= 0)
        {
            PhotonNetwork.Destroy(currentPlayer.gameObject);
        }
        else
        {
            currentPlayer.Health = health;
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        float randomValueX = Random.Range(5.10f, -2.61f);
        float randomValueY = Random.Range(83f, 85f);
        float randomValueZ = Random.Range(-80f, -93f);
        GameObject obj = PhotonNetwork.Instantiate("Player_Network", new Vector3(randomValueX, randomValueY, randomValueZ), Quaternion.identity, 0);
        currentPlayer = obj.GetComponent<PlayerMovement>();
    }

    private IEnumerator C_SetPing()
    {
        while (PhotonNetwork.IsConnected)
        {
            playerCustomProperties["Ping"] = PhotonNetwork.GetPing();
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);

            yield return new WaitForSeconds(5f);
        }
    }

    public override void OnConnectedToMaster()
    {
        if (pingCoroutine != null)
        {
            StopCoroutine(pingCoroutine);
        }
        pingCoroutine = StartCoroutine(C_SetPing());
    }

    public void SetName(string playerName)
    {
        this.playerName = playerName;
        PhotonNetwork.NickName = playerName;
    }

    public string PlayerName
    {
        get { return playerName; }
    }
}*/
