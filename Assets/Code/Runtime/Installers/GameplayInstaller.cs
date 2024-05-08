using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem;
using Code.Runtime.Service;
using Code.Runtime.UI.Windows;
using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private NetworkRunner networkRunner;
        [SerializeField] private NetworkPlayersHandler networkPlayersHandler;
        [SerializeField] private PlayerRig playerRig;
        [SerializeField] private GameTime gameTime;
        
        public override void InstallBindings()
        {
            BindGameplayBootstrapper();

            BindStatesFactory();

            BindPlayerFactory();

            BindNetworkPlayersHandler();

            BindWindowFactory();

            BindWindowService();

            BindGameTime();
            
            Container.BindInstance(networkRunner);
            
            Container.BindInstance(playerRig);
        }

        private void BindGameTime()
        {
            Container
                .BindInterfacesTo<GameTime>()
                .FromInstance(gameTime)
                .AsSingle();
        }

        private void BindWindowService()
        {
            Container.BindInterfacesTo<WindowService>().AsSingle();
        }

        private void BindWindowFactory()
        {
            Container.BindInterfacesTo<WindowFactory>().AsSingle();
        }

        private void BindNetworkPlayersHandler()
        {
            Container
                .BindInterfacesTo<NetworkPlayersHandler>()
                .FromInstance(networkPlayersHandler)
                .AsSingle();
        }

        private void BindPlayerFactory()
        {
            Container.BindInterfacesTo<PlayerFactory>().AsSingle();
        }

        private void BindGameplayBootstrapper()
        {
            Container.Bind<GameplayStateMachine>().AsSingle();
        }
        
        private void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }
    }
}