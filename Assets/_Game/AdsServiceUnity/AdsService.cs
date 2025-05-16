using System;
using _Game.SDKService;
using UnityEngine;
using Zenject;

namespace _Game.AdsServiceUnity
{
    public class AdsService : IInitializable, IDisposable, IAdsService
    {
        private const string ANDROIDID = "5856151";
        private const string IOSID = "5856150";
        
        
        public void Initialize()
        {
            InitService();
        }

        public void Dispose()
        {
        }


        public void InitService()
        {
            Debug.Log("AdsService.InitService()");
        }

        public void ShowAdsForReward()
        {
            Debug.Log("AdsService.ShowAdsForReward()");
        }

        public void ShowPassiveAds()
        {
            Debug.Log("AdsService.ShowPassiveAds()");
        }
    }
}