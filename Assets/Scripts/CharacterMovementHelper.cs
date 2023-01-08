using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    public class CharacterMovementHelper : MonoBehaviour
    {
        private XROrigin XROrigin;
        private CharacterController characterController;
        private CharacterControllerDriver driver;

        private void Start()
        {
            XROrigin = GetComponent<XROrigin>();
            characterController = GetComponent<CharacterController>();
            driver = GetComponent<CharacterControllerDriver>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateCharacterController();
        }

        /// <summary>
        /// Updates the <see cref="CharacterController.height"/> and <see cref="CharacterController.center"/>
        /// based on the camera's position.
        /// </summary>
        protected virtual void UpdateCharacterController()
        {
            if (XROrigin == null || characterController == null)
                return;

            var height = Mathf.Clamp(XROrigin.CameraInOriginSpaceHeight, driver.minHeight, driver.maxHeight);

            Vector3 center = XROrigin.CameraInOriginSpacePos;
            center.y = height / 2f + characterController.skinWidth;

            characterController.height = height;
            characterController.center = center;
        }
    }
}