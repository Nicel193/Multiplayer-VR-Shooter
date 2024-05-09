using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Gameplay;
using Fusion.Addons.ConnectionManagerAddon;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.Bootstrappers
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        [SerializeField] private ConnectionManager connectionManager;
        
        [Inject] private GameplayStateMachine _gameplayStateMachine;
        [Inject] private IStatesFactory _statesFactory;
        [Inject] private ISceneLoader _sceneLoader;

        private void Awake()
        {
            if (!_sceneLoader.IsNameLoadedScene(SceneName.Gameplay.ToString())) return;
            
            AddGameplayStates();
            
            connectionManager.StartConnection();
        }

        private void Update() =>
            _gameplayStateMachine.UpdateState();

        private void AddGameplayStates()
        {
            _gameplayStateMachine.RegisterState(_statesFactory.Create<GameLoopState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<EnemyLeftState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<LoadState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<MathEndState>());
        }
    }
}