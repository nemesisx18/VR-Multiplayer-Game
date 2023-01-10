using UnityEngine;
using UnityEngine.UI;

namespace VR_Multiplayer.GameScene.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private Button _closeUIButton;
        [SerializeField] private Button[] _webinarButtons;
        [SerializeField] private Button[] _webinarCloseButtons;

        [SerializeField] private GameObject _menuCanvas;
        [SerializeField] private GameObject[] _webinarPanels;

        private bool _isUIActive;
        private bool _isWebinarActive;

        private void Start()
        {
            _closeUIButton.onClick.AddListener(ToggleMenu);
            SetWebinarButtonListener();
        }

        private void SetWebinarButtonListener()
        {
            for (int i = 0; i < _webinarButtons.Length; i++)
            {
                int tempIndex = i;
                _webinarButtons[i].onClick.AddListener(() => ToggleWebinarMenu(tempIndex));
                _webinarCloseButtons[i].onClick.AddListener(() => ToggleWebinarMenu(tempIndex));
            }
        }

        private void ToggleWebinarMenu(int tempIndex)
        {
            _isWebinarActive = !_isWebinarActive;

            _webinarPanels[tempIndex].SetActive(_isWebinarActive);
            ToggleMenu();
        }

        private void ToggleMenu()
        {
            _isUIActive = !_isUIActive;
            _menuCanvas.SetActive(_isUIActive);
        }
    }
}