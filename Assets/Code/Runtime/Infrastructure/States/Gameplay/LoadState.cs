using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem;
using Fusion;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class LoadState : IPayloadedState<PlayerRef>
    {
        private IPlayerFactory _playerFactory;
        private INetworkPlayersHandler _networkPlayersHandler;
        private PlayerRig _playerRig;

        private LoadState(IPlayerFactory playerFactory, INetworkPlayersHandler networkPlayersHandler, PlayerRig playerRig)
        {
            _playerRig = playerRig;
            _networkPlayersHandler = networkPlayersHandler;
            _playerFactory = playerFactory;
        }
        
        public void Enter(PlayerRef playerRef)
        {
            _playerFactory.CreatePlayer(playerRef);
            _networkPlayersHandler.AddPlayerInTeam(playerRef);
        }

        public void Exit()
        {
            
        }
    }
}