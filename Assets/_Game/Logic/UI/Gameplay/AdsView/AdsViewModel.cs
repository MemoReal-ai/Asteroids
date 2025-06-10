using System;
using _Game.AdsServiceUnity;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using _Game.Logic.Gameplay.Enemy;
using R3;
using Zenject;

namespace _Game.Gameplay.Logic.UI.AdsView
{
    public class AdsViewModel : IInitializable, IDisposable
    {
        public ReactiveCommand HidePopupCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand ShowAdsCommand { get; private set; } = new ReactiveCommand();

        private readonly IRewardedAdsHandler _adsRewardedAdsHandler;
        private readonly IInterstitialAds _interstitialAdsHandler;
        private readonly Spawner _spawner;
        private readonly GameTimeHandler _gameTimeHandler;
        private readonly ShipAbstract _ship;

        public AdsViewModel(IRewardedAdsHandler adsRewardedAdsHandler, Spawner spawner, GameTimeHandler gameTimeHandler,
            ShipAbstract ship, IInterstitialAds interstitialAds)
        {
            _interstitialAdsHandler = interstitialAds;
            _adsRewardedAdsHandler = adsRewardedAdsHandler;
            _spawner = spawner;
            _gameTimeHandler = gameTimeHandler;
            _ship = ship;
        }

        public void Initialize()
        {
            ShowAdsCommand.Subscribe(x => ShowAdsForReward());
            HidePopupCommand.Subscribe(x => ShowPassiveAds());
        }

        public void Dispose()
        {
            ShowAdsCommand?.Dispose();
            HidePopupCommand?.Dispose();
        }

        private void ShowAdsForReward()
        {
            _spawner.DisableAllEnemies();
            _adsRewardedAdsHandler.ShowAds();
            _gameTimeHandler.Unpause();
        }

        private void ShowPassiveAds()
        {
            _interstitialAdsHandler.ShowAds();
            _ship.InvokeLoseLastLife();
        }
    }
}