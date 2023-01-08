using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int SceneIndex;
    public int MaxPlayer;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _buttonUI;

    public List<DefaultRoom> DefaultRooms;

    private const string ROOM_NAME = "Room 1";

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect to server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server.");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("We have joined the lobby");
        _buttonUI.SetActive(true);
    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = DefaultRooms[defaultRoomIndex];

        PhotonNetwork.LoadLevel(roomSettings.SceneIndex);
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.MaxPlayer;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        Debug.Log(PhotonNetwork.PlayerList.Length);
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
