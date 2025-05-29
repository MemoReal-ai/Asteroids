namespace _Game.Purchasing_Service
{
    public interface IPurchasingService
    {
        void Buy(EnumPurchasing purchasing);
        bool HasPurchasingAdsSkip();
        void SetFlagPurchasingAdsSkip(bool skip);
    }
}