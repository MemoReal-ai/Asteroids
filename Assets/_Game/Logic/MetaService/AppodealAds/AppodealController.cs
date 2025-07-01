using _Game.Logic.MetaService.AdsServiceUnity;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Game.Logic.MetaService.AppodealAds
{
    public class AppodealController : IInitializable, IAdsService
    {
        private const bool TOGGLE_TESTING_MOD = true;
        private const string KEY_APP = "3c86a3ec4e9633158cd1e931f10449f9ddec206709fe0685";

        public void Initialize()
        {
            Appodeal.Cache(AppodealAdType.RewardedVideo);
            Appodeal.SetTesting(TOGGLE_TESTING_MOD);
            Appodeal.MuteVideosIfCallsMuted(true);
            Appodeal.Initialize(KEY_APP, AppodealAdType.Interstitial | AppodealAdType.RewardedVideo);
        }

        public void ShowAdsForReward(string idAds, IUnityAdsShowListener _ = null)
        {
            if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
            {
                Debug.Log("Appodeal is loaded rewarded video");
                Appodeal.Show(AppodealShowStyle.RewardedVideo);
            }
        }

        public void ShowPassiveAds(string idAds, IUnityAdsShowListener _ = null)
        {
            if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
            {
                Debug.Log("Appodeal is loaded passive ads");
                Appodeal.Show(AppodealAdType.Interstitial);
            }
        }
    }
}