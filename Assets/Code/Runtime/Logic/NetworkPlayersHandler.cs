using System.Linq;
using System.Threading.Tasks;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States.Gameplay;
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

        private PlayerRef _localPlayer;
        private PlayerRig _playerRig;
        private bool _isSpawned;
        private GameplayStateMachine _gameplayStateMachine;

        [Inject]
        private void Construct(PlayerRig playerRig, GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _playerRig = playerRig;
        }
        
        public void MovePlayerInStartPosition()
        {
            _playerRig.transform.position = GetPlayerSpawnPosition(_localPlayer);
        }

        public async void AddPlayer(PlayerRef playerRef)
        {
            await Object.WaitObjectSpawned();

            _localPlayer = playerRef;
            
            AddPlayerInTeam(_localPlayer);
            MovePlayerInStartPosition();
        }

        public void RemovePlayer(PlayerRef playerRef)
        {
            if(TeamsPlayers.ContainsKey(playerRef))
            {
                TeamsPlayers.Remove(playerRef);

                Team winTeam = TeamsPlayers
                    .Take(1)
                    .Select(d => d.Value)
                    .First();

                if (TeamsPlayers.Count == 1)
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