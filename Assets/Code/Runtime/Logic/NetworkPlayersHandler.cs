using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States.Gameplay;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class NetworkPlayersHandler : NetworkBehaviour, INetworkRunnerCallbacks, INetworkPlayersHandler
    {
        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedCharacters =
            new Dictionary<PlayerRef, NetworkObject>();

        private readonly List<NetworkObject> _activePlayers = new List<NetworkObject>();

        private GameplayStateMachine _gameplayStateMachine;
        private NetworkRunner _networkRunner;

        private void OnEnable()
        {
            if (_networkRunner != null)
            {
                _networkRunner.AddCallbacks(this);
            }
        }

        private void OnDisable()
        {
            if (_networkRunner != null)
            {
                _networkRunner.RemoveCallbacks(this);
            }
        }

        [Inject]
        private void Construct(GameplayStateMachine gameplayStateMachine, NetworkRunner networkRunner)
        {
            _networkRunner = networkRunner;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void AddNetworkPlayer(PlayerRef player, NetworkObject playerObject)
        {
            _spawnedCharacters.Add(player, playerObject);
            _activePlayers.Add(playerObject);
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("Player joined: " + player.PlayerId);

            if (Runner.Topology != Topologies.ClientServer && player == Runner.LocalPlayer)
            {
                _gameplayStateMachine.Enter<LoadState, PlayerRef>(player);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                Runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key,
            ArraySegment<byte> data)
        {
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
        }
    }

    public interface INetworkPlayersHandler
    {
    }
}