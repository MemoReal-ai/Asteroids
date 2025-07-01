using System.Collections.Generic;
using _Game.AdsServiceUnity;
using _Game.FirebaseService;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Logic.Effects;
using _Game.Logic.Gameplay.Service.Sound;
using _Game.Logic.Infrastructure.EntryPoints;
using _Game.Logic.MetaService.Addressable;
using _Game.Logic.MetaService.AdsServiceUnity;
using _Game.Logic.MetaService.AppodealAds;
using _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler;
using _Game.Logic.MetaService.DataServices.SaveAndLoadHandler;
using _Game.Logic.MetaService.FirebaseService;
using _Game.Logic.MetaService.JsonConvertService;
using _Game.Logic.MetaService.Purchasing_Service;
using _Game.Logic.MetaService.SceneTransitionerService;
using AppodealStack.Monetization.Api;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.Project
{
    public class ProjectInstallerGame : MonoInstaller
    {
        [SerializeField] private SoundHandler _soundHandler;
        [SerializeField] private ParticleHandler _particleHandler;

        private readonly List<ISaver> _savers = new List<ISaver>();

        public override void InstallBindings()
        {
            BindAnalytics();
            BindAddressableService();
            BindAdsService();
            BindRemoteConfigProvider();
            BindPurchasingService();
            BindAuthenticationService();
            BindJsonConverter();
            BindSaverService();
            BindSoundService();
            BindParticleHandler();

            Container.BindInterfacesTo<EntryPointProject>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneTransitioner>().AsCached();
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle();
        }

        private void BindParticleHandler()
        {
            Container.Bind<ParticleHandler>().FromComponentInNewPrefab(_particleHandler).AsCached().NonLazy();
        }

        private void BindSoundService()
        {
            Container.Bind<SoundHandler>().FromComponentInNewPrefab(_soundHandler).AsCached();
        }

        private void BindJsonConverter()
        {
            Container.BindInterfacesAndSelfTo<JsonConverterService>().AsCached().NonLazy();
        }

        private void BindAuthenticationService()
        {
            Container.BindInterfacesAndSelfTo<MetaService.AuthenticatorService.AuthenticatorService>().AsCached()
                .NonLazy();
        }

        private void BindSaverService()
        {
            Container.BindInterfacesAndSelfTo<DataSyncManager>().AsCached();
            Container.BindInterfacesAndSelfTo<LocalSaver>().AsCached();
            Container.BindInterfacesAndSelfTo<CloudSaver>().AsCached();

            _savers.Add(Container.Resolve<LocalSaver>());
            _savers.Add(Container.Resolve<CloudSaver>());
            Container.Bind<List<ISaver>>().FromInstance(_savers).AsCached();
        }

        private void BindPurchasingService()
        {
            Container.BindInterfacesAndSelfTo<PurchasingService>().AsCached();
        }

        private void BindRemoteConfigProvider()
        {
            Container.BindInterfacesAndSelfTo<RemoteConfigProvider>().AsSingle();
        }

        private void BindAdsService()
        {
            Container.BindInterfacesAndSelfTo<AppodealController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AppodealRewardedAds>().AsSingle();
            Container.BindInterfacesAndSelfTo<AppodealInterstitialAds>().AsSingle();
        }

        private void BindAddressableService()
        {
            Container.BindInterfacesAndSelfTo<AddressableLoader>().AsCached().NonLazy();
        }

        private void BindAnalytics()
        {
            Container.BindInterfacesAndSelfTo<InitFirebaseServiceAnalytics>().AsCached();
        }
    }
}