using Code.Runtime.Configs;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Installers
{
    [CreateAssetMenu(fileName = "CoreConfig", menuName = "Configs/CoreConfig", order = 0)]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private WindowsConfig windowsConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(playerConfig);
            Container.BindInstance(gameConfig);
            Container.BindInstance(windowsConfig);
        }
    }
}