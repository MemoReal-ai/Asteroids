using UnityEngine.Advertisements;

namespace _Game.Logic.MetaService.AdsServiceUnity
{
    public interface IAdsService
    {
        void ShowAdsForReward(string idAds, IUnityAdsShowListener listener = null);
        void ShowPassiveAds(string idAds, IUnityAdsShowListener listener = null);
    }
}