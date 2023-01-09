using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using VR_Multiplayer.LobbyScene.UI;

namespace VR_Multiplayer.GameScene.Networking
{
    [System.Serializable]
    public class DefaultRoom
    {
        public string Name;
        public int MaxPlayer;
        public int SceneIndex;
    }

    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public List<DefaultRoom> DefaultRooms;
        public delegate void NetworkDelegate();
        public static event NetworkDelegate OnPlayerJoinedLobby;

        public override void OnEnable()
        {
            base.OnEnable();
            LobbyUI.OnPlayerConnected += ConnectToServer;
            LobbyUI.OnPlayerJoinedRoom += InitializeRoom;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            LobbyUI.OnPlayerConnected -= ConnectToServer;
            LobbyUI.OnPlayerJoinedRoom -= InitializeRoom;
        }

        public void ConnectToServer()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            OnPlayerJoinedLobby?.Invoke();
        }

        public void InitializeRoom(int defaultRoomIndex)
        {
            DefaultRoom roomSettings = DefaultRooms[defaultRoomIndex];

            PhotonNetwork.LoadLevel(roomSettings.SceneIndex);

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = (byte)roomSettings.MaxPlayer;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;

            PhotonNetwork.JoinOrCreateRoom(roomSettings.Name, roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined a Room");
            base.OnJoinedRoom();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
        }
    }
}