using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Logic.PlayerSystem;
using ExitGames.Client.Photon;
using Fusion;
using Fusion.XR.Shared;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class NetworkPlayersHandler : NetworkBehaviour, INetworkPlayersHandler
    {
        [SerializeField] private PlayerSpawnPosition redTeamSpawn;
        [SerializeField] private PlayerSpawnPosition blueTeamSpawn;
        
        [Networked, Capacity(10)]
        private NetworkDictionary<PlayerRef, Team> TeamsPlayers => default;

        private GameplayStateMachine _gameplayStateMachine;
        private NetworkRunner _networkRunner;
        private PlayerRef _localPlayer;
        private PlayerRig _playerRig;

        [Inject]
        private void Construct(GameplayStateMachine gameplayStateMachine, NetworkRunner networkRunner, PlayerRig playerRig)
        {
            _playerRig = playerRig;
            _networkRunner = networkRunner;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public override void Spawned()
        {
            if(_localPlayer.IsNone) return;

            AddPlayerInTeam(_localPlayer);
            
            _playerRig.transform.position = GetPlayerSpawnPosition(_localPlayer);
        }

        public void AddPlayer(PlayerRef playerRef)
        {
            _localPlayer = playerRef;
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