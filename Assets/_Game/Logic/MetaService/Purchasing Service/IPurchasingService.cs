using System;
using R3;

namespace _Game.Logic.MetaService.Purchasing_Service
{
    public interface IPurchasingService
    { 
        bool HasPurchasingAdsSkip();
        void SetFlagPurchasingAdsSkip(bool state);
        event Action<bool> OnBuyRemoveAds;
        ReactiveCommand BuyRemoveAdsCommand { get; }
    }
}