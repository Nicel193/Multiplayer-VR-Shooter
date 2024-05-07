using Code.Runtime.Logic.WeaponSystem;
using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic.PlayerSystem
{
    [RequireComponent(typeof(NetworkPlayerRig), typeof(PlayerHealth))]
    public class NetworkPlayer : NetworkBehaviour
    {
        public PlayerHealth PlayerHealth { get; private set; }
        public BaseWeapon PlayerWeapon { get; private set; }

        private NetworkPlayerRig _networkPlayerRig;
        private PlayerRig _playerRig;

        private void Awake()
        {
            _networkPlayerRig = GetComponent<NetworkPlayerRig>();
            PlayerHealth = GetComponent<PlayerHealth>();
        }

        public override void Spawned()
        {
            if(!Object.HasStateAuthority) return;

            _playerRig = FindObjectOfType<PlayerRig>();
            
            _networkPlayerRig.Initialize(_playerRig);
            _playerRig.RightHand.selectEntered.AddListener(SelectWeapon);
            _playerRig.RightHand.selectExited.AddListener(RemoveWeapon);
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _playerRig.RightHand.selectEntered.RemoveListener(SelectWeapon);
            _playerRig.RightHand.selectExited.RemoveListener(RemoveWeapon);
        }

        private void SelectWeapon(SelectEnterEventArgs arg)
        {
            if(arg.interactableObject.transform.TryGetComponent(out BaseWeapon baseWeapon))
                PlayerWeapon = baseWeapon;
        }

        private void RemoveWeapon(SelectExitEventArgs arg0) =>
            PlayerWeapon = null;
    }
}