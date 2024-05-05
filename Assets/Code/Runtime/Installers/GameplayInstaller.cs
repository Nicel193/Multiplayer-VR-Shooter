using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem;
using Zenject;

namespace Code.Runtime.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameplayBootstrapper();

            BindStatesFactory();

            BindPlayerFactory();
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