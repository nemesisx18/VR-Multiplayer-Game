using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VR_Multiplayer.GameScene.Networking;

namespace VR_Multiplayer.LobbyScene.UI
{
    public class LobbyUI : MonoBehaviour
    {
        [Header("Main Menu")]
        [SerializeField] private GameObject _menuPanel;

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _connectButton;
        [SerializeField] private Button _closeMenuButton;
        [SerializeField] private Button[] _openRoomPanelButtons;
        [SerializeField] private Button[] _closeRoomPanelButtons;

        [Header("Webinar Menu")]
        [SerializeField] private Button[] _joinRoomButtons;
        [SerializeField] private GameObject[] _roomPanels;

        private bool _isMenuOpen;
        private bool _isMenuButtonActive;
        private bool[] _isRoomPanelOpen;

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
            _isRoomPanelOpen = new bool[_roomPanels.Length];
            
            _isMenuOpen = _menuPanel.activeInHierarchy;
            _isMenuButtonActive = _menuButton.gameObject.activeInHierarchy;

            for (int i = 0; i < _roomPanels.Length; i++)
            {
                _isRoomPanelOpen[i] = _roomPanels[i].activeInHierarchy;
            }
            
            _menuButton.onClick.AddListener(ToggleMenuButton);
            _connectButton.onClick.AddListener(ConnectToServer);
            _closeMenuButton.onClick.AddListener(ToggleMenuButton);

            SetRoomButtonListener();
            SetRoomSelectButtonListener();
        }

        private void ConnectToServer()
        {
            OnPlayerConnected?.Invoke();
            _connectButton.gameObject.SetActive(false);
        }

        private void SetActiveButton()
        {
            ToggleMenuPanel();
            _menuButton.gameObject.SetActive(false);
        }

        private void SetRoomButtonListener()
        {
            for (int i = 0; i < _joinRoomButtons.Length; i++)
            {
                int tempIndex = i;
                _joinRoomButtons[i].onClick.AddListener(() => OnClickRoomButton(tempIndex));
            }
        }

        private void SetRoomSelectButtonListener()
        {
            for (int i = 0; i < _openRoomPanelButtons.Length; i++)
            {
                int tempIndex = i;
                _openRoomPanelButtons[i].onClick.AddListener(() => OnSelectRoomButton(tempIndex));
                _closeRoomPanelButtons[i].onClick.AddListener(() => OnSelectRoomButton(tempIndex));
            }
        }

        private void OnClickRoomButton(int buttonIndex)
        {
            OnPlayerJoinedRoom?.Invoke(buttonIndex);
        }

        private void OnSelectRoomButton(int buttonIndex)
        {
            _isRoomPanelOpen[buttonIndex] = !_isRoomPanelOpen[buttonIndex];

            _roomPanels[buttonIndex].gameObject.SetActive(_isRoomPanelOpen[buttonIndex]);
            ToggleMenuPanel();
        }

        private void ToggleMenuPanel()
        {
            _isMenuOpen = !_isMenuOpen;
            _menuPanel.SetActive(_isMenuOpen);
        }

        private void ToggleMenuButton()
        {
            _isMenuButtonActive = !_isMenuButtonActive;
            _menuButton.gameObject.SetActive(_isMenuButtonActive);

            ToggleMenuPanel();
        }
    }
}
