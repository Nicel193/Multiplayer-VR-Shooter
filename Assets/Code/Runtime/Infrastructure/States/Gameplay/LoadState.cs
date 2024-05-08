using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem;
using Code.Runtime.UI.Windows;
using Fusion;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class LoadState : IPayloadedState<PlayerRef>
    {
        private IPlayerFactory _playerFactory;
        private INetworkPlayersHandler _networkPlayersHandler;
        private IWindowFactory _windowFactory;

        private LoadState(IPlayerFactory playerFactory, INetworkPlayersHandler networkPlayersHandler, IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
            _networkPlayersHandler = networkPlayersHandler;
            _playerFactory = playerFactory;
        }
        
        public void Enter(PlayerRef playerRef)
        {
            INetworkPlayer networkPlayer = _playerFactory.CreatePlayer(playerRef);
            
            _networkPlayersHandler.AddPlayer(playerRef, networkPlayer);
            _windowFactory.Initialize(networkPlayer.WindowPosition);
        }

        public void Exit()
        {
            
        }
    }
}