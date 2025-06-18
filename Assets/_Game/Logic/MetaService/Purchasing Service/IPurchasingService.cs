using System;

namespace _Game.Purchasing_Service
{
    public interface IPurchasingService
    {
        void BuyRemoveAds();
        bool HasPurchasingAdsSkip();
        void SetFlagPurchasingAdsSkip(bool state);
        event Action<bool> OnBuyRemoveAds;
    }
}