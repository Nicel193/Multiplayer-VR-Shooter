using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem;
using Fusion;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class LoadState : IPayloadedState<PlayerRef>
    {
        private IPlayerFactory _playerFactory;

        private LoadState(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        
        public void Enter(PlayerRef playerRef)
        {
            _playerFactory.CreatePlayer(playerRef);
        }

        public void Exit()
        {
            
        }
    }
}