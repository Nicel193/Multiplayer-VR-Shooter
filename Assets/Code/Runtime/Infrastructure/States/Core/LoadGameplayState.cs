using Fusion;
using UnityEngine;
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
            
            await _networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = _gameMode,
                SessionName = _sessionName,
                Scene = CurrentSceneInfo(),
                PlayerCount = MaxSessionPlayers,
                SceneManager = _networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
        }

        public void Exit()
        {
            
        }

        private NetworkSceneInfo CurrentSceneInfo()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneRef sceneRef = default;

            if (activeScene.buildIndex < 0 || activeScene.buildIndex >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogError("Current scene is not part of the build settings");
            }
            else
            {
                sceneRef = SceneRef.FromIndex(activeScene.buildIndex);
            }

            var sceneInfo = new NetworkSceneInfo();
            if (sceneRef.IsValid)
            {
                sceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Single);
            }
            return sceneInfo;
        }
    }
}