using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Repositories;
using Fusion;

namespace Code.Runtime.Infrastructure.States.Core
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IInteractorContainer _interactorContainer;

        LoadProgressState(GameStateMachine gameStateMachine, IInteractorContainer interactorContainer)
        {
            _gameStateMachine = gameStateMachine;
            _interactorContainer = interactorContainer;
        }

        public void Enter()
        {
            _gameStateMachine
                .Enter<LoadGameplayState, (string sessionName, GameMode gameMode)>(("Test", GameMode.Shared));
        }

        public void Exit()
        {
        }
    }
}