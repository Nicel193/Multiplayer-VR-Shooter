using Code.Runtime.Logic.WeaponSystem;
using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic.PlayerSystem
{
    [RequireComponent(typeof(NetworkPlayerRig), typeof(PlayerData), typeof(CapsuleCollider))]
    public class NetworkPlayer : NetworkBehaviour, INetworkPlayer
    {
        [field: SerializeField] public Transform WindowPosition { get; private set; }

        public PlayerData PlayerData { get; private set; }
        public BaseWeapon PlayerWeapon { get; private set; }

        private NetworkPlayerRig _networkPlayerRig;
        private CapsuleCollider _capsuleCollider;
        private PlayerRig _playerRig;

        private void Awake()
        {
            _networkPlayerRig = GetComponent<NetworkPlayerRig>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            PlayerData = GetComponent<PlayerData>();
        }

        public override void Spawned()
        {
            if(!Object.HasStateAuthority) return;

            _capsuleCollider.enabled = false;
            
            InitializePlayerRig();
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if(_playerRig == null) return;
            
            _playerRig.RightHand.selectEntered.RemoveListener(SelectWeapon);
            _playerRig.RightHand.selectExited.RemoveListener(RemoveWeapon);
            _playerRig.LeftHand.selectEntered.RemoveListener(SelectWeapon);
            _playerRig.LeftHand.selectExited.RemoveListener(RemoveWeapon);
        }

        private void InitializePlayerRig()
        {
            _playerRig = FindObjectOfType<PlayerRig>();

            _networkPlayerRig.Initialize(_playerRig);

            _playerRig.RightHand.selectEntered.AddListener(SelectWeapon);
            _playerRig.RightHand.selectExited.AddListener(RemoveWeapon);
            _playerRig.LeftHand.selectEntered.AddListener(SelectWeapon);
            _playerRig.LeftHand.selectExited.AddListener(RemoveWeapon);
        }

        private void SelectWeapon(SelectEnterEventArgs arg)
        {
            if (arg.interactableObject.transform.TryGetComponent(out BaseWeapon baseWeapon))
            {
                PlayerWeapon = baseWeapon;
                
                PlayerWeapon.Initialize(PlayerData);
            }
        }

        private void RemoveWeapon(SelectExitEventArgs arg0) =>
            PlayerWeapon = null;
    }
}