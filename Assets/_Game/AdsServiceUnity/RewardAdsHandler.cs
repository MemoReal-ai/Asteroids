using _Game.SDKService;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;
using Application = UnityEngine.Device.Application;

namespace _Game.AdsServiceUnity
{
    public class RewardAdsHandler : IInitializable, IRewardedAdsHandler, IUnityAdsShowListener, IUnityAdsLoadListener
    {
        private const string REWARDEDANDROADS = "Rewarded_Android";
        private const string REWARDEDIOS = "Rewarded_IOS";

        private readonly IAdsService _adsService;

        private string _adsId;

        public RewardAdsHandler(IAdsService adsService)
        {
            _adsService = adsService;
        }

        public void Initialize()
        {
            _adsId = Application.platform == RuntimePlatform.IPhonePlayer ? REWARDEDIOS : REWARDEDANDROADS;
            LoadAds();
        }

        public void LoadAds()
        {
            Advertisement.Load(_adsId, this);
        }

        public void ShowAds()
        {
            _adsService.ShowAdsForReward(_adsId, this);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"Unity Ads show failure: {error}, message: {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log($"Unity Ads show start: {placementId}");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log($"Unity Ads show click: {placementId}");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            LoadAds();
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"Unity Ads ad loaded: {placementId}");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.LogError($"Unity Ads failed to load: {error}, message: {message}");
        }
    }
}