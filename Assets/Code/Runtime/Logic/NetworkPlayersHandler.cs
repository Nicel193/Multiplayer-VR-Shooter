using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States.Gameplay;
using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class NetworkPlayersHandler : NetworkBehaviour, IPlayerJoined, IPlayerLeft, INetworkPlayersHandler
    {
        public event Action OnPlayerJoined;
        
        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
        private readonly List<NetworkObject> _activePlayers = new List<NetworkObject>();
        
        private GameplayStateMachine _gameplayStateMachine;

        [Inject]
        private void Construct(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void AddNetworkPlayer(PlayerRef player, NetworkObject playerObject)
        {
            _spawnedCharacters.Add(player, playerObject);
            _activePlayers.Add(playerObject);
        }

        public void PlayerJoined(PlayerRef player)
        {
            if (player != Runner.LocalPlayer) return;
            
            _gameplayStateMachine.Enter<LoadState, PlayerRef>(player);
            
            OnPlayerJoined?.Invoke();
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                Runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
        }
    }

    public interface INetworkPlayersHandler
    {
    }
}