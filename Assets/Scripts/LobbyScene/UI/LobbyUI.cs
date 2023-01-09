using UnityEngine;
using UnityEngine.UI;
using VR_Multiplayer.GameScene.Networking;

namespace VR_Multiplayer.LobbyScene.UI
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonUI;
        [SerializeField] private Button _connectButton;
        [SerializeField] private Button[] _roomButtons;

        public delegate void ButtonHandler();
        public delegate void NetworkHandler(int index);
        public static event ButtonHandler OnPlayerConnected;
        public static event NetworkHandler OnPlayerJoinedRoom;

        private void OnEnable()
        {
            NetworkManager.OnPlayerJoinedLobby += SetActiveButton;
        }

        private void OnDisable()
        {
            NetworkManager.OnPlayerJoinedLobby -= SetActiveButton;
        }

        private void Start()
        {
            _connectButton.onClick.AddListener(ConnectToServer);
            SetRoomButtonListener();
        }

        private void ConnectToServer()
        {
            OnPlayerConnected?.Invoke();
            _connectButton.gameObject.SetActive(false);
        }

        private void SetActiveButton()
        {
            _buttonUI.SetActive(true);
        }

        private void SetRoomButtonListener()
        {
            for (int i = 0; i < _roomButtons.Length; i++)
            {
                int tempIndex = i;
                _roomButtons[i].onClick.AddListener(() => OnClickRoomButton(tempIndex));
            }
        }

        private void OnClickRoomButton(int buttonIndex)
        {
            OnPlayerJoinedRoom?.Invoke(buttonIndex);
        }
    }
}
