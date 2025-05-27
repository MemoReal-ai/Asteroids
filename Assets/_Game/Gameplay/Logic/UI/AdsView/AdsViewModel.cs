using System;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using _Game.SDKService;
using R3;
using Zenject;

namespace _Game.Gameplay.Logic.UI.AdsView
{
    public class AdsViewModel : IInitializable, IDisposable
    {
        public ReactiveCommand HidePopupCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand ShowAdsCommand { get; private set; } = new ReactiveCommand();

        private readonly IAdsService _adsService;
        private readonly Spawner _spawner;
        private readonly GameTimeHandler _gameTimeHandler;
        private readonly ShipAbstract _ship;

        public AdsViewModel(IAdsService adsService, Spawner spawner, GameTimeHandler gameTimeHandler, ShipAbstract ship)
        {
            _adsService = adsService;
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
            _adsService.ShowAdsForReward();
            _gameTimeHandler.Unpause();
        }

        private void ShowPassiveAds()
        {
            _adsService.ShowPassiveAds();
            _ship.InvokeLoseLastLife();
        }
    }
}