using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;

        private PlayerRig _playerRig;
        private Vector3 _leftHandOffset;
        private Vector3 _rightHandOffset;

        private bool IsLocalNetworkRig => Object && Object.HasInputAuthority;

        public override void Spawned()
        {
            if(!IsLocalNetworkRig) return;

            _playerRig = FindObjectOfType<PlayerRig>();

            _leftHandOffset = leftHand.eulerAngles;
            _rightHandOffset = rightHand.eulerAngles;
        }

        private void LateUpdate()
        {
            if(_playerRig == null) return;

            Vector3 xROriginCameraPosition = _playerRig.Camera.transform.position;

            transform.position = new Vector3(xROriginCameraPosition.x, 0f, xROriginCameraPosition.z);
            
            leftHand.transform.position = _playerRig.LeftHand.position;
            leftHand.transform.rotation = _playerRig.LeftHand.rotation;
            leftHand.transform.eulerAngles += _leftHandOffset;

            rightHand.transform.position = _playerRig.RightHand.position;
            rightHand.transform.rotation = _playerRig.RightHand.rotation;
            rightHand.transform.eulerAngles += _rightHandOffset;
        }
    }
}