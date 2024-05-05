using Fusion;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Infrastructure.States.Core
{
    public class LoadGameplayState : IPayloadedState<(string sessionName, GameMode gameMode)>
    {
        private const int MaxSessionPlayers = 3;
        
        private NetworkRunner _networkRunner;
        private ISceneLoader _sceneLoader;
        private GameMode _gameMode;
        private string _sessionName;

        public LoadGameplayState(NetworkRunner networkRunner, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _networkRunner = networkRunner;
        }

        public void Enter((string sessionName, GameMode gameMode) payload)
        {
            _sessionName = payload.sessionName;
            _gameMode = payload.gameMode;
            
            _sceneLoader.Load(SceneName.Gameplay.ToString(), SceneLoaded);
        }

        private async void SceneLoaded()
        {
            _networkRunner.ProvideInput = true;
            
            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid)
            {
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
            }
            
            await _networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = _gameMode,
                SessionName = _sessionName,
                Scene = scene,
                PlayerCount = MaxSessionPlayers,
                SceneManager = _networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
        }

        public void Exit()
        {
            
        }
    }
}