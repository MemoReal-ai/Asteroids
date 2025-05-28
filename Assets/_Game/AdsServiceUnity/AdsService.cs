using System;
using _Game.SDKService;
using UnityEngine;
using Zenject;
using UnityEngine.Advertisements;

namespace _Game.AdsServiceUnity
{
    public class AdsService : IInitializable, IAdsService, IUnityAdsInitializationListener
    {
        private const string ANDROIDID = "5856151";
        private const string IOSID = "5856150";

        private bool _isTestMod = true;

        public void Initialize()
        {
            InitService();
        }

        public void InitService()
        {
            if (Advertisement.isSupported)
            {
                Advertisement.Initialize(Application.platform == RuntimePlatform.IPhonePlayer ? IOSID : ANDROIDID,
                    _isTestMod,
                    this);
            }
        }

        public void ShowAdsForReward(string idAds, IUnityAdsShowListener listener)
        {
            Advertisement.Show(idAds, listener);
            Debug.Log("AdsService.ShowAdsForReward()");
        }

        public void ShowPassiveAds(string idAds, IUnityAdsShowListener listener)
        {
            Advertisement.Show(idAds, listener);
            Debug.Log("AdsService.ShowPassiveAds()");
        }

        public void OnInitializationComplete() { }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("OnInitializationFailed()");
        }
    }
}