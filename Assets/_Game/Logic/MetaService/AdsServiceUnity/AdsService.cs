using _Game.Logic.MetaService.Purchasing_Service;
using _Game.SDKService;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Game.Logic.MetaService.AdsServiceUnity
{
    public class AdsService : IInitializable, IAdsService, IUnityAdsInitializationListener
    {
        private const string ANDROID_ID = "5856151";
        private const string IOS_ID = "5856150";

        private readonly IPurchasingService _purchasingService;
        private readonly bool _isTestMod = true;

        public AdsService(IPurchasingService purchasingService)
        {
            _purchasingService = purchasingService;
        }

        public void Initialize()
        {
            InitService();
        }


        public void ShowAdsForReward(string idAds, IUnityAdsShowListener listener)
        {
            if (!_purchasingService.HasPurchasingAdsSkip())
            {
                Advertisement.Show(idAds, listener);
                Debug.Log("AdsService.ShowAdsForReward()");
            }
        }

        public void ShowPassiveAds(string idAds, IUnityAdsShowListener listener)
        {
            if (!_purchasingService.HasPurchasingAdsSkip())
            {
                Advertisement.Show(idAds, listener);
                Debug.Log("AdsService.ShowPassiveAds()");
            }
        }

        public void OnInitializationComplete()
        {
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("OnInitializationFailed()");
        }

        private void InitService()
        {
            if (Advertisement.isSupported)
            {
                Advertisement.Initialize(Application.platform == RuntimePlatform.IPhonePlayer ? IOS_ID : ANDROID_ID,
                    _isTestMod,
                    this);
            }
        }
    }
}