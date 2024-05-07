using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Logic.PlayerSystem;
using Fusion;
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
            Team teamColor = TeamsPlayers.Count % 2 == 0 ? Team.Blue : Team.Red;
            
            TeamsPlayers.Add(_localPlayer, teamColor);
            
            _playerRig.transform.position = GetPlayerSpawnPosition(_localPlayer);
        }

        public void AddPlayerInTeam(PlayerRef playerRef)
        {
            _localPlayer = playerRef;
        }

        public Vector3 GetPlayerSpawnPosition(PlayerRef playerRef)
        {
            Team teamColor = TeamsPlayers[playerRef];

            if (teamColor == Team.Red)
                return redTeamSpawn.GetSpawnPosition();

            return blueTeamSpawn.GetSpawnPosition();
        }
    }
}