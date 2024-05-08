using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States.Gameplay;
using Code.Runtime.Logic.PlayerSystem;
using Fusion;
using UnityEngine;
using Zenject;
using NetworkPlayer = Code.Runtime.Logic.PlayerSystem.NetworkPlayer;

namespace Code.Runtime.Logic
{
    public class NetworkPlayersHandler : NetworkBehaviour, INetworkPlayersHandler
    {
        public INetworkPlayer LocalNetworkPlayer { get; private set; }

        [SerializeField] private PlayerSpawnPosition redTeamSpawn;
        [SerializeField] private PlayerSpawnPosition blueTeamSpawn;
        
        [Networked, Capacity(10)]
        private NetworkDictionary<PlayerRef, Team> TeamsPlayers => default;
        private Dictionary<PlayerRef, Team> LocalTeamsPlayers = new Dictionary<PlayerRef, Team>();
        
        private PlayerRig _playerRig;
        private GameplayStateMachine _gameplayStateMachine;
        private bool _isSpawned;

        [Inject]
        private void Construct(PlayerRig playerRig, GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _playerRig = playerRig;
        }

        public void MovePlayerInStartPosition(PlayerRef playerRef)
        {
            _playerRig.transform.position = GetPlayerSpawnPosition(playerRef);
        }
        
        public async void AddPlayer(PlayerRef playerRef, INetworkPlayer networkPlayer)
        {
            await Runner.WaitObjectSpawned();
            
            RPC_AddPlayer(playerRef);
            
            if (LocalNetworkPlayer == null)
            {
                MovePlayerInStartPosition(playerRef);

                LocalNetworkPlayer = networkPlayer;
            }
        }

        [Rpc]
        private void RPC_AddPlayer(PlayerRef playerRef)
        {
            AddPlayerInTeam(playerRef);
            InitializeLocalTeamsPlayers();
        }

        private void InitializeLocalTeamsPlayers()
        {
            LocalTeamsPlayers = TeamsPlayers
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public void RemovePlayer(PlayerRef playerRef)
        {
            if(LocalTeamsPlayers.ContainsKey(playerRef))
            {
                LocalTeamsPlayers.Remove(playerRef);

                Team winTeam = LocalTeamsPlayers
                    .Take(1)
                    .Select(d => d.Value)
                    .First();

                if (LocalTeamsPlayers.Count == 1)
                    _gameplayStateMachine.Enter<EndGameState, Team>(winTeam);
            }
        }

        private void AddPlayerInTeam(PlayerRef playerRef)
        {
            Team teamColor = TeamsPlayers.Count % 2 == 0 ? Team.Blue : Team.Red;
            
            TeamsPlayers.Add(playerRef, teamColor);
        }

        private Vector3 GetPlayerSpawnPosition(PlayerRef playerRef)
        {
            Team teamColor = TeamsPlayers[playerRef];

            if (teamColor == Team.Red)
                return redTeamSpawn.GetSpawnPosition();

            return blueTeamSpawn.GetSpawnPosition();
        }
    }
}