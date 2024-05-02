using Code.Runtime.Configs;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Installers
{
    [CreateAssetMenu(fileName = "CoreConfig", menuName = "Configs/CoreConfig", order = 0)]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(playerConfig);
        }
    }
}