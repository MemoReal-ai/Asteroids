using System;
using _Game.Purchasing_Service;
using Zenject;
using R3;

namespace _Game.MainMenu.Logic.UI.Store
{
    public class StoreViewModel : IInitializable, IDisposable
    {
        public ReadOnlyReactiveProperty<bool> IsAdsRemoved = new ReactiveProperty<bool>();
        public ReactiveCommand BuyCommand { get; private set; } = new ReactiveCommand();

        private readonly IPurchasingService _purchasingService;

        public StoreViewModel(IPurchasingService purchasingService)
        {
            _purchasingService = purchasingService;
        }

        public void Initialize()
        {
            BuyCommand.Subscribe(x => _purchasingService.BuyRemoveAds());
            
            IsAdsRemoved = Observable.
                EveryValueChanged(this, _ => !_purchasingService.HasPurchasingAdsSkip()).
                ToReadOnlyReactiveProperty();
            
        }

        public void Dispose()
        {
            IsAdsRemoved?.Dispose();
            BuyCommand?.Dispose();
        }
    }
}