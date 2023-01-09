using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace VR_Multiplayer.GameScene.Players
{
    public class HandPresence : MonoBehaviour
    {
        public bool showController = false;
        public InputDeviceCharacteristics controllerCharacteristics;
        public List<GameObject> controllerPrefabs;
        public GameObject handModelPrefab;

        private InputDevice targetDevice;
        private GameObject spawnedController;
        private GameObject spawnedHandModel;
        private Animator handAnimator;

        private const string TRIGGER_ANIM = "Trigger";
        private const string GRIP_ANIM = "Grip";

        private void Start()
        {
            TryInitialize();
        }

        private void TryInitialize()
        {
            List<InputDevice> devices = new List<InputDevice>();

            InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

            foreach (var item in devices)
            {
                Debug.Log(item.name + item.characteristics);
            }

            if (devices.Count > 0)
            {
                targetDevice = devices[0];
                GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
                if (prefab)
                {
                    spawnedController = Instantiate(prefab, transform);
                }
                else
                {
                    Debug.Log("Did not find corresponding controller model");
                }

                spawnedHandModel = Instantiate(handModelPrefab, transform);
                handAnimator = spawnedHandModel.GetComponent<Animator>();
            }
        }

        private void UpdateHandAnimation()
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                handAnimator.SetFloat(TRIGGER_ANIM, triggerValue);
            }
            else
            {
                handAnimator.SetFloat(TRIGGER_ANIM, 0);
            }

            if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                handAnimator.SetFloat(GRIP_ANIM, gripValue);
            }
            else
            {
                handAnimator.SetFloat(GRIP_ANIM, 0);
            }
        }

        private void Update()
        {
            if (!targetDevice.isValid)
            {
                TryInitialize();
            }
            else
            {
                if (showController)
                {
                    if (spawnedHandModel)
                        spawnedHandModel.SetActive(false);
                    if (spawnedController)
                        spawnedController.SetActive(true);
                }
                else
                {
                    if (spawnedHandModel)
                        spawnedHandModel.SetActive(true);
                    if (spawnedController)
                        spawnedController.SetActive(false);
                    UpdateHandAnimation();
                }
            }
        }
    }
}