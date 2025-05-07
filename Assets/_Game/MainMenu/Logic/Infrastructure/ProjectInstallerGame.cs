using _Game.Firebase;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Infrastructure;
using _Game.Gameplay.Logic.Service;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class ProjectInstallerGame : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            BindSDK();

            Container.BindInterfacesTo<EntryPointProject>().AsSingle();
            Container.Bind<SceneHandler>().AsCached();
            Container.BindInterfacesTo<DataSaver>().AsCached();
            Container.BindInterfacesAndSelfTo<DataHandler>().AsCached().NonLazy();
            
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle();
            Container.DeclareSignal<EnemyDiedSignal>();

        }

        private void BindSDK()
        {
            Container.BindInterfacesAndSelfTo<InitServiceSDK>().AsCached();
        }
    }
}