using UnityEngine.Advertisements;

namespace _Game.SDKService
{
    public interface IAdsService
    {
        void ShowAdsForReward(string idAds, IUnityAdsShowListener listener);
        void ShowPassiveAds(string idAds, IUnityAdsShowListener listener);
    }
}