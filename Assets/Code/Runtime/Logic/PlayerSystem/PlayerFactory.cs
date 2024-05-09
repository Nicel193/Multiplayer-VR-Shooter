using Code.Runtime.Configs;
using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerFactory : IPlayerFactory
    {
        private NetworkRunner _networkRunner;
        private PlayerConfig _playerConfig;
        private DiContainer _diContainer;

        public PlayerFactory(NetworkRunner networkRunner, PlayerConfig playerConfig, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _playerConfig = playerConfig;
            _networkRunner = networkRunner;
        }
        
        public NetworkPlayer CreatePlayer(PlayerRef playerRef)
        {
            NetworkPlayer networkPlayer =
                _networkRunner.Spawn(_playerConfig.NetworkPlayerPrefab, Vector3.zero, Quaternion.identity, playerRef);

            _diContainer.InjectGameObject(networkPlayer.gameObject);
            
            networkPlayer.GetComponent<PlayerData>().Initialize(_playerConfig.MaxPlayerHealth, playerRef);
            
            return networkPlayer;
        }
    }
}