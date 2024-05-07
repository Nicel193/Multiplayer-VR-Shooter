using Code.Runtime.Configs;
using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerFactory : IPlayerFactory
    {
        private NetworkRunner _networkRunner;
        private PlayerConfig _playerConfig;

        public PlayerFactory(NetworkRunner networkRunner, PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
            _networkRunner = networkRunner;
        }
        
        public NetworkPlayer CreatePlayer(PlayerRef playerRef)
        {
            NetworkPlayer networkPlayer =
                _networkRunner.Spawn(_playerConfig.NetworkPlayerPrefab, Vector3.zero, Quaternion.identity, playerRef);

            networkPlayer.GetComponent<PlayerHealth>().Initialize(_playerConfig.MaxPlayerHealth);
            
            return networkPlayer;
        }
    }
}