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
        
        private LoadState(IPlayerFactory playerFactory, INetworkPlayersHandler networkPlayersHandler)
        {
            _networkPlayersHandler = networkPlayersHandler;
            _playerFactory = playerFactory;
        }
        
        public void Enter(PlayerRef playerRef)
        {
            _playerFactory.CreatePlayer(playerRef);
            _networkPlayersHandler.AddPlayer(playerRef);
        }

        public void Exit()
        {
            
        }
    }
}