using _Game.Addressable;
using _Game.AdsServiceUnity;
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

            BindAnalytics();
            BindAddressableService();
            BindAdsService();

            Container.BindInterfacesTo<EntryPointProject>().AsSingle();
            Container.Bind<SceneHandler>().AsCached();
            Container.BindInterfacesTo<DataSaver>().AsCached();
            Container.BindInterfacesAndSelfTo<DataHandler>().AsCached().NonLazy();

            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle();
            Container.DeclareSignal<EnemyDiedSignal>();
        }

        private void BindAdsService()
        {
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RewardAdsHandler>().AsCached();
            Container.BindInterfacesAndSelfTo<InterstitialAdsHandler>().AsCached();
        }

        private void BindAddressableService()
        {
            Container.BindInterfacesAndSelfTo<AddressableLoader>().AsCached().NonLazy();
        }

        private void BindAnalytics()
        {
            Container.BindInterfacesAndSelfTo<InitServiceAnalytics>().AsCached();
        }
    }
}