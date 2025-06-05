using System;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using Zenject;
using R3;

namespace _Game.Gameplay.Logic.UI.AdsView
{
    public class AdsBinder : IInitializable, IDisposable
    {
        private readonly AdsPopupView _adsPopupView;
        private readonly AdsViewModel _adsViewModel;
        private readonly ShipAbstract _ship;
        private readonly GameTimeHandler _gameTimeHandler;

        public AdsBinder(AdsPopupView adsPopupView, AdsViewModel adsViewModel, ShipAbstract ship, GameTimeHandler gameTimeHandler)
        {
            _adsPopupView = adsPopupView;
            _adsViewModel = adsViewModel;
            _ship = ship;
            _gameTimeHandler = gameTimeHandler;
        }

        public void Initialize()
        {
            _ship.OnShipDestroyedToRewardAds += Show;
            _gameTimeHandler.OnResume += Hide;
            _gameTimeHandler.OnLoseGame += Hide;

            _adsPopupView.AdsButton
                .OnClickAsObservable()
                .Subscribe(_adsViewModel.ShowAdsCommand.Execute)
                .AddTo(_adsPopupView);

            _adsPopupView.ExitButton
                .OnClickAsObservable()
                .Subscribe(_adsViewModel.HidePopupCommand.Execute)
                .AddTo(_adsPopupView);
        }

        public void Dispose()
        {
            _ship.OnShipDestroyedToRewardAds -= Show;
            _gameTimeHandler.OnResume -= Hide;
            _gameTimeHandler.OnLoseGame -= Hide;
        }

        private void Hide()
        {
            _adsPopupView.Hide();
        }

        private void Show()
        {
            _gameTimeHandler.PauseToAds();
            _adsPopupView.Show();
            
        }
    }
}