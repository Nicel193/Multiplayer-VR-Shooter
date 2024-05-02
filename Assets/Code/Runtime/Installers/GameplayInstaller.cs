using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.StateMachines;
using Zenject;

namespace Code.Runtime.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameplayBootstrapper();

            BindStatesFactory();
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