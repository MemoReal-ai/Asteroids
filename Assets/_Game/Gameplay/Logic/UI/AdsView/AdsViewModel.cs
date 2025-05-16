using System;
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

        public AdsViewModel(IAdsService adsService)
        {
            _adsService = adsService;
        }

        public void Initialize()
        {
            ShowAdsCommand.Subscribe(x => _adsService.ShowAdsForReward());
            HidePopupCommand.Subscribe(x => _adsService.ShowPassiveAds());
        }

        public void Dispose()
        {
            ShowAdsCommand?.Dispose();
            HidePopupCommand?.Dispose();
        }
    }
}