using Code.Runtime.Repositories;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class GameLoopState : IState, IUpdatebleState
    {
        private const int TimeToAddScore = 1;
        private const int ScoreInOneInterval = 1;
        
        private float _scoreTimer;
        private bool _isFirstEntry = true;

        public GameLoopState(IInteractorContainer interactorContainer)
        {

        }

        public void Enter()
        {

        }

        public void Update()
        {

        }

        public void Exit()
        {
            
        }
    }
}