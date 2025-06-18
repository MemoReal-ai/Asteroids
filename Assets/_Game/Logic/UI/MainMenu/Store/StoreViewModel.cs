using System;
using _Game.Purchasing_Service;
using R3;
using Zenject;

namespace _Game.Logic.UI.MainMenu.Store
{
    public class StoreViewModel : IInitializable, IDisposable
    {
        public ReactiveProperty<bool> IsAdsRemoved = new ReactiveProperty<bool>();
        public ReactiveCommand BuyCommand { get; private set; } = new ReactiveCommand();

        private readonly IPurchasingService _purchasingService;

        public StoreViewModel(IPurchasingService purchasingService)
        {
            _purchasingService = purchasingService;
        }

        public void Initialize()
        {
            _purchasingService.OnBuyRemoveAds += UpdateStateReactiveProperty;
            UpdateStateReactiveProperty(_purchasingService.HasPurchasingAdsSkip());

            BuyCommand.Subscribe(x => _purchasingService.BuyRemoveAds());
        }

        public void Dispose()
        {
            _purchasingService.OnBuyRemoveAds -= UpdateStateReactiveProperty;
            IsAdsRemoved?.Dispose();
            BuyCommand?.Dispose();
        }

        private void UpdateStateReactiveProperty(bool state)
        {
            IsAdsRemoved.Value = !state;
        }
    }
}