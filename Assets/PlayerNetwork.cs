/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;

public class PlayerNetwork : MonoBehaviour
{
    [SerializeField] private string level;
    public static PlayerNetwork Instance;
    private string playerName;
    private PhotonView PhotonView;
    private int PlayersInGame = 0;
    private ExitGames.Client.Photon.Mashtable m_playerCustomProperties = new ExitGames.Client.Photon.Mashtable();
    private PlayerMovement CurrentPlayer;
    private Coroutine m_pingCoroutine;
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();

        playerName = "Player#" + Random.Range(1000, 9999);

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == level)
        {
            if(PhotonNetwork.isMasterClient)
            {
                MasterLoadedGame();
            }
            else{
                NonMasterLoadedGame();
            }
            
        }
    }

    private void MasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGamesScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(level);
    }

    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        Debug.Log("RPC_LoadedGameScene");
        PlayersInGame++;
        if (PlayersInGame == PhotonNetwork.playerList.Length)
        {
            Debug.Log("All players are in the game scene");
            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }

    public void NewHealth(PhotonPlayer photonPlayer, int health)
    {
        PhotonView.RPC("RPC_NewHealth", photonPlayer, health);
    }

    [PunRPC]
    private void RPC_NewHealth(int health)
    {
        if (CurrentPlayer == null)
        {
            return;
        }
        if (health <=0)
        {
            PhotonNetwork.Destroy(CurrentPlayer.gameObject);
        }
        else
        {
            CurrentPlayer.Health = health;
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        float randomValueX = Random.Range(5.10f, -2.61f);
        float randomValueY = Random.Range(83f, 85f);
        float randomValueZ = Random.Range(-80f, -93f);
        GameObject obj = PhotonNetwork.Instantiate("Player_Netwrok", new Vector3(randomValueX, randomValueY, randomValueZ), Quaternion.identity, 0);
        CurrentPlayer = obj.GetComponent<PlayerMovement>();
    }

    private IEnumerator C_SetPing()
    {
        while (PhotonNetwork.connected)
        {
            m_playerCustomProperties["Ping"] = PhotonNetwork.GetPing();
            PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);

            yield return new WaitForSeconds(5f);
        }

        yield break;
    }

    private void OnConnectedToMaster()
    {
        if (m_pingCoroutine != null)
        {
            StopCoroutine(m_pingCoroutine);
            m_pingCoroutine = StartCoroutine(C_SetPing());
        }
    }

    private void SetName(string playerName)
    {
        this.playerName = playerNames;
        PhotonNetwork.playerName = playerName;
    }

    public string PlayerName
    {
        get { return playerName; }
    }
}*/
