using Photon.Pun;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject _spawnedPlayerPrefab;
    private const string PREFAB_NAME = "Player Models/Network Player ";

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        int randomNumbers = Random.Range(1, 6);
        _spawnedPlayerPrefab = PhotonNetwork.Instantiate(PREFAB_NAME + randomNumbers, transform.position, transform.rotation);
        //_spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);

        PhotonNetwork.NickName = "Player " + PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(_spawnedPlayerPrefab);
    }
}
