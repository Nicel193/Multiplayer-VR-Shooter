using Code.Runtime.Infrastructure.StateMachines;
using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class NetworkPlayersHandler : NetworkBehaviour, INetworkPlayersHandler
    {
        [SerializeField] private PlayerSpawnPosition redTeamSpawn;
        [SerializeField] private PlayerSpawnPosition blueTeamSpawn;
        
        [Networked, Capacity(2)]
        private NetworkDictionary<PlayerRef, Team> TeamsPlayers => default;

        private GameplayStateMachine _gameplayStateMachine;
        private NetworkRunner _networkRunner;

        [Inject]
        private void Construct(GameplayStateMachine gameplayStateMachine, NetworkRunner networkRunner)
        {
            _networkRunner = networkRunner;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void AddPlayerInTeam(PlayerRef playerRef)
        {
            Team teamColor = TeamsPlayers.Count % 2 == 0 ? Team.Blue : Team.Red;
            
            TeamsPlayers.Add(playerRef, teamColor);
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