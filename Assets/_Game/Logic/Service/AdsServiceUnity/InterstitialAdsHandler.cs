using _Game.SDKService;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Game.AdsServiceUnity
{
    public class InterstitialAdsHandler : IInitializable, IInterstitialAds, IUnityAdsShowListener, IUnityAdsLoadListener
    {
        private const string INTERSTITIALANDROID = "Interstitial_Android";
        private const string INTERSTITIALIOS = "Interstitial_IOS";

        private readonly IAdsService _adsService;

        private string _interstitialAdsId;

        public InterstitialAdsHandler(IAdsService adsService)
        {
            _adsService = adsService;
        }

        public void Initialize()
        {
            _interstitialAdsId = Application.platform == RuntimePlatform.IPhonePlayer
                ? INTERSTITIALIOS
                : INTERSTITIALANDROID;
        }

        public void ShowAds()
        {
            _adsService.ShowPassiveAds(_interstitialAdsId, this);
        }

        private void LoadAds()
        {
            Advertisement.Load(_interstitialAdsId, this);
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