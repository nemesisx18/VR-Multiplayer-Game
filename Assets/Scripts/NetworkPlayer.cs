using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private Animator _leftHandAnimator;
    [SerializeField] private Animator _rightHandAnimator;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Text _name;

    private Transform _bodyRig;
    private Transform _leftHandRig;
    private Transform _rightHandRig;

    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();

        XROrigin rig = FindObjectOfType<XROrigin>();

        _bodyRig = rig.transform.Find("Camera Offset/Main Camera");
        _leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        _rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");
        _name.text = _photonView.Owner.NickName;

        if (_photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            MapPosition(_body, _bodyRig);
            MapPosition(_leftHand, _leftHandRig);
            MapPosition(_rightHand, _rightHandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), _leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), _rightHandAnimator);
        }
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }

    private void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
}
