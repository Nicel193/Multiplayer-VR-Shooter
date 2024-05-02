using Code.Runtime.Repositories;

namespace Code.Runtime.Infrastructure.States.Core
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IInteractorContainer _interactorContainer;

        public LoadSceneState(ISceneLoader sceneLoader, IInteractorContainer interactorContainer)
        {
            _interactorContainer = interactorContainer;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName);
        }

        public void Exit()
        {
        }
    }
}