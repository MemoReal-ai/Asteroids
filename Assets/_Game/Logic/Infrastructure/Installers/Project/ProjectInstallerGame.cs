using _Game.AdsServiceUnity;
using _Game.FirebaseService;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.SaveAndLoadHandler;
using _Game.Logic.Gameplay.Service.Sound;
using _Game.Logic.MetaService.Addressable;
using _Game.Logic.MetaService.AuthenticatorService;
using _Game.Logic.MetaService.FirebaseService;
using _Game.Purchasing_Service;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class ProjectInstallerGame : MonoInstaller
    {
        [SerializeField] private SoundHandler _soundHandler;

        public override void InstallBindings()
        {
            BindAnalytics();
            BindAddressableService();
            BindAdsService();
            BindRemoteConfigProvider();
            BindPurchasingService();
            BindSaverService();
            BindAuthenticationService();
            BindJsonConverter();
            BindSoundService();

            Container.BindInterfacesTo<EntryPointProject>().AsSingle();
            Container.Bind<SceneHandler>().AsCached();
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle();
        }

        private void BindSoundService()
        {
            Container.Bind<SoundHandler>().FromComponentInNewPrefab(_soundHandler).AsCached();
        }

        private void BindJsonConverter()
        {
            Container.BindInterfacesAndSelfTo<JsonConverterHandler>().AsCached().NonLazy();
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