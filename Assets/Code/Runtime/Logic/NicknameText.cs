using UnityEngine;

namespace Code.Runtime.Logic
{
    public class NicknameText : MonoBehaviour
    {
        private Transform _mainCameraTransform;

        private void Start() =>
            _mainCameraTransform = Camera.main.transform;

        private void Update() =>
            transform.LookAt(_mainCameraTransform);
    }
}