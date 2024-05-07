using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.PlayerSystem
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : NetworkBehaviour
    {
        private PlayerHealth _playerHealth;
        private INetworkPlayersHandler _networkPlayersHandler;

        [Inject]
        private void Construct(INetworkPlayersHandler networkPlayersHandler)
        {
            _networkPlayersHandler = networkPlayersHandler;
        }

        private void Awake() =>
            _playerHealth = GetComponent<PlayerHealth>();

        private void OnEnable() =>
            _playerHealth.OnPlayerDead += OnDeath;

        private void OnDisable() =>
            _playerHealth.OnPlayerDead -= OnDeath;

        private void OnDeath()
        {
            if(!Object.HasStateAuthority) return;
            
            _playerHealth.RPC_ResumeHealth();
            _networkPlayersHandler.MovePlayerInStartPosition();
        }
    }
}