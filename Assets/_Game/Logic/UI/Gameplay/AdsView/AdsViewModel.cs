using System;
using _Game.AdsServiceUnity;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.UI.AdsView;
using _Game.Logic.Gameplay.Enemy;
using R3;
using Zenject;

namespace _Game.Logic.UI.Gameplay.AdsView
{
    public class AdsViewModel : IInitializable, IDisposable
    {
        private ReactiveCommand HidePopupCommand { get;} = new();
        private ReactiveCommand ShowAdsCommand { get;} = new();

        private readonly IRewardedAdsHandler _adsRewardedAdsHandler;
        private readonly IInterstitialAds _interstitialAdsHandler;
        private readonly Spawner _spawner;
        private readonly GameTimeHandler _gameTimeHandler;
        private readonly ShipAbstract _ship;
        private readonly AdsPopupView _adsPopupView;

        public AdsViewModel(IRewardedAdsHandler adsRewardedAdsHandler, Spawner spawner, GameTimeHandler gameTimeHandler,
            ShipAbstract ship, IInterstitialAds interstitialAds, AdsPopupView adsPopupView)
        {
            _interstitialAdsHandler = interstitialAds;
            _adsRewardedAdsHandler = adsRewardedAdsHandler;
            _spawner = spawner;
            _gameTimeHandler = gameTimeHandler;
            _ship = ship;
            _adsPopupView = adsPopupView;
        }

        public void Initialize()
        {
            _ship.OnShipDestroyedToRewardAds += ShowPopupView;
            _gameTimeHandler.OnResume += _adsPopupView.Hide;
            _gameTimeHandler.OnLoseGame += _adsPopupView.Hide;

            ShowAdsCommand.Subscribe(x => ShowAdsForReward());
            HidePopupCommand.Subscribe(x => ShowPassiveAds());

            Bind();
        }


        public void Dispose()
        {
            ShowAdsCommand?.Dispose();
            HidePopupCommand?.Dispose();

            _ship.OnShipDestroyedToRewardAds -= ShowPopupView;
            _gameTimeHandler.OnResume -= _adsPopupView.Hide;
            _gameTimeHandler.OnLoseGame -= _adsPopupView.Hide;
        }

        private void Bind()
        {
            _adsPopupView.AdsButton.OnClickAsObservable().Subscribe(ShowAdsCommand.Execute).AddTo(_adsPopupView);
            _adsPopupView.ExitButton.OnClickAsObservable().Subscribe(HidePopupCommand.Execute).AddTo(_adsPopupView);
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

        private void ShowPopupView()
        {
            _gameTimeHandler.PauseToAds();
            _adsPopupView.Show();
        }
    }
}