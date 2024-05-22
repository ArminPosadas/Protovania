using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom room;
    private PhotonView PV;
    public bool IsGameLoad;
    public int currentScene;

    private Photon.Realtime.Player[] photonPlayers;
    public int playersInRooms;
    public int myNumbersInRoom;
    public int playerInGame;

    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayer;
    private float atMaxPlayer;
    private float timeToStart;

    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
            }
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void Start()
    {
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayer = startingTime;
        atMaxPlayer = 6;
        timeToStart = startingTime;
    }

    void Update()
    {
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            if (playersInRooms == 1)
            {
                RestartTimer();
            }
            if (!IsGameLoad)
            {
                if (readyToStart)
                {
                    atMaxPlayer -= Time.deltaTime;
                    lessThanMaxPlayer = atMaxPlayer;
                    timeToStart = atMaxPlayer;
                }
                else if (readyToCount) 
                { 
                    lessThanMaxPlayer -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayer;
                }
                Debug.Log("Displayer time to start to the players" + timeToStart);
                if (timeToStart <=0)
                {
                    StartGame();
                }
            }
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("se uni al joinedroom");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRooms = photonPlayers.Length;
        myNumbersInRoom = playersInRooms;
        PhotonNetwork.NickName = myNumbersInRoom.ToString();
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            Debug.Log("Displayer players in room out of max players possible (" + playersInRooms + ": " + MultiplayerSettings.multiplayerSettings.maxPlayers + ")");
            if(playersInRooms > 1)
            {
                readyToCount = true;
            }
            if (playersInRooms == MultiplayerSettings.multiplayerSettings.maxPlayers)
            {
                readyToStart = true;
                if (PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        else
        {
            StartGame();
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRooms++;
        if(MultiplayerSettings.multiplayerSettings.delayStart)
        {
            Debug.Log("Displayer players in room out of max players possible (" + playersInRooms + ": " + MultiplayerSettings.multiplayerSettings.maxPlayers + ")");
            if (playersInRooms > 1)
            {
                readyToCount = true;
            }
            if (playersInRooms == MultiplayerSettings.multiplayerSettings.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen=false;
            }
        }
    }
    void StartGame()
    {
        IsGameLoad = true;
        if (!PhotonNetwork.IsMasterClient) 
            return;
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiplayerSettings.multiplayerSettings.multiplayerScene);
    }

    void RestartTimer()
    {
        lessThanMaxPlayer = startingTime;
        timeToStart = startingTime;
        atMaxPlayer = 6;
        readyToCount = false;
        readyToStart = false;
    }
    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == MultiplayerSettings.multiplayerSettings.multiplayerScene)
        {
         IsGameLoad = true;
            if (MultiplayerSettings.multiplayerSettings.delayStart)
            {
                PV.RPC("RPC_loadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                RPC_CreatePlayer();
            }
        }
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playerInGame++;
        if (playerInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        Debug.Log("se creo el jugador");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player2"), transform.position, Quaternion
            .identity, 0);
    }
}