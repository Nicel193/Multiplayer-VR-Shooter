using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.PlayerSystem
{
    [RequireComponent(typeof(PlayerData))]
    public class PlayerDeath : NetworkBehaviour
    {
        private PlayerData _playerData;
        private INetworkPlayersHandler _networkPlayersHandler;

        [Inject]
        private void Construct(INetworkPlayersHandler networkPlayersHandler)
        {
            _networkPlayersHandler = networkPlayersHandler;
        }

        private void Awake() =>
            _playerData = GetComponent<PlayerData>();

        private void OnEnable() =>
            _playerData.OnPlayerDead += OnDeath;

        private void OnDisable() =>
            _playerData.OnPlayerDead -= OnDeath;

        private void OnDeath()
        {
            if(!Object.HasStateAuthority) return;
            
            _playerData.RPC_ResumeHealth();
            _networkPlayersHandler.MovePlayerInStartPosition(Runner.LocalPlayer);
        }
    }
}