using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class NetworkPlayerRig : NetworkBehaviour
    {
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;

        private PlayerRig _playerRig;
        private Vector3 _leftHandOffset;
        private Vector3 _rightHandOffset;
        
        public void Initialize(PlayerRig playerRig)
        {
            _playerRig = playerRig;

            _leftHandOffset = leftHand.eulerAngles;
            _rightHandOffset = rightHand.eulerAngles;
        }
        
        private void LateUpdate()
        {
            if(_playerRig == null) return;

            Vector3 xROriginCameraPosition = _playerRig.Camera.transform.position;

            transform.position = new Vector3(xROriginCameraPosition.x, 0f, xROriginCameraPosition.z);
            
            leftHand.transform.position = _playerRig.LeftHand.transform.position;
            leftHand.transform.rotation = _playerRig.LeftHand.transform.rotation;
            leftHand.transform.eulerAngles += _leftHandOffset;

            rightHand.transform.position = _playerRig.RightHand.transform.position;
            rightHand.transform.rotation = _playerRig.RightHand.transform.rotation;
            rightHand.transform.eulerAngles += _rightHandOffset;
        }
    }
}