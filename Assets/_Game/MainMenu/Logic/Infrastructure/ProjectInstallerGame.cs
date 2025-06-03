using _Game.Addressable;
using _Game.AdsServiceUnity;
using _Game.AuthenticatorService;
using _Game.Firebase;
using _Game.FirebaseService;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Infrastructure;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.SaveAndLoadHandler;
using _Game.Purchasing_Service;
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
            BindRemoteConfigProvider();
            BindPurchasingService();
            BindSaverService();
            BindAuthenticationService();

            Container.BindInterfacesTo<EntryPointProject>().AsSingle();
            Container.Bind<SceneHandler>().AsCached();
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle();
            Container.DeclareSignal<EnemyDiedSignal>();
        }

        private void BindAuthenticationService()
        {
            Container.BindInterfacesAndSelfTo<AuthenticatorHandler>().AsCached().NonLazy();
        }

        private void BindSaverService()
        {
            Container.BindInterfacesAndSelfTo<DataHandler>().AsCached();
            Container.BindInterfacesTo<LocalSaver>().AsCached();
            Container.BindInterfacesTo<CloudSaver>().AsCached();
        }

        private void BindPurchasingService()
        {
            Container.BindInterfacesAndSelfTo<PurchasingHandler>().AsCached();
        }

        private void BindRemoteConfigProvider()
        {
            Container.BindInterfacesAndSelfTo<RemoteConfigProvider>().AsSingle();
        }

        private void BindAdsService()
        {
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RewardAdsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<InterstitialAdsHandler>().AsSingle();
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