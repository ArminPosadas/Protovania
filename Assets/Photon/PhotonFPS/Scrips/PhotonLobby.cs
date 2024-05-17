using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

public static PhotonLobby lobby;
public GameObject battleButton;
RoomInfo[] room;
public GameObject cancelButton;

private void Awake()
{
    lobby = this;
}
void Start()
{
    //PhotonNetwork.ConnecUsingSettings();
}

public override void OnConnectedToMaster()
{
    Debug.Log("la coneccion esta establecida");
    PhotonNetwork.AutomaticallySyncScene = true;
    battleButton.SetActive(true);
}

public void OnBattleButtonClicked()
{
    Debug.Log("paso algo");
    battleButton.SetActive(false);
    cancelButton.SetActive(true);
    PhotonNetwork.JoinRandomRoom();
}

public override void OnJoinRandomFailed(short returnCode, string message)
{
    Debug.Log("falo en conectar a la sala");
    CreateRoom();
}

void CreateRoom()
{
    Debug.Log("funciona Createroom");
    //int randomRoomName = randomRoomName.Range(0,10000);
   // RoomOptions roomOps = new RoomOptions()
    //{
       // IsVisible = true;
       // IsOpen = true;
        //MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayers

   // };
    //PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps, null);

}

public override void OnCreateRoomFailed(short returnCode, string message)
{
    Debug.Log("fallo en conectar a la sala");
    CreateRoom();
}

public void OnCancelButtonCliked()
{
    Debug.Log("funciona OnCancelButtonCliked");
    battleButton.SetActive(false);
    cancelButton.SetActive(true);
    PhotonNetwork.LeaveRoom();
}

}
