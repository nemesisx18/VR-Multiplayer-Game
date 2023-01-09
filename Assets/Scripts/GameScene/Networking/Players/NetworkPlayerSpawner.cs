using Photon.Pun;
using UnityEngine;

namespace VR_Multiplayer.GameScene.Networking.Players
{
    public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
    {
        private GameObject _spawnedPlayerPrefab;

        private const string PLAYER_NAME = "Player ";
        private const string PREFAB_NAME = "Player Models/Network Player ";

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            int randomNumbers = Random.Range(1, 6);
            _spawnedPlayerPrefab = PhotonNetwork.Instantiate(PREFAB_NAME + randomNumbers, transform.position, transform.rotation);

            PhotonNetwork.NickName = PLAYER_NAME + PhotonNetwork.CurrentRoom.PlayerCount;
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            PhotonNetwork.Destroy(_spawnedPlayerPrefab);
        }
    }
}