using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.PlayerSystem
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : MonoBehaviour
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
            _playerHealth.RPC_ResumeHealth();
            _networkPlayersHandler.MovePlayerInStartPosition();
        }
    }
}