using Code.Runtime.Logic.PlayerSystem;
using Fusion;
using TMPro;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class NicknameText : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI nicknameText;
        [SerializeField] private PlayerData playerData;

        private Transform _mainCameraTransform;
        
        public override void FixedUpdateNetwork()
        {
            nicknameText.text = playerData.Nickname;
        }

        private void Start() =>
            _mainCameraTransform = Camera.main.transform;

        private void Update() =>
            transform.LookAt(_mainCameraTransform);
    }
}