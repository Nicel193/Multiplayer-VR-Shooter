using Code.Runtime.Logic;
using Code.Runtime.Service;
using Code.Runtime.UI;
using Code.Runtime.UI.Windows;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class EndGameState : IPayloadedState<Team>
    {
        private IWindowService _windowService;

        public EndGameState(IWindowService windowService)
        {
            _windowService = windowService;
        }
        
        public void Enter(Team winTeam)
        {
            _windowService.OpenPayloadWindow<EndGameWindow, Team>(winTeam);
        }

        public void Exit()
        {
            
        }
    }
}