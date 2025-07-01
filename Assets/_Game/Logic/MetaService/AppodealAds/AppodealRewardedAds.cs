using _Game.AdsServiceUnity;
using _Game.Logic.MetaService.AdsServiceUnity;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

namespace _Game.Logic.MetaService.AppodealAds
{
    public class AppodealRewardedAds : IRewardedAdsHandler, IRewardedVideoAdListener
    {
        private readonly IAdsService _adsService;

        public AppodealRewardedAds(IAdsService adsService)
        {
            _adsService = adsService;
        }

        public void ShowAds()
        {
            _adsService.ShowAdsForReward(null);
        }

        public void OnRewardedVideoLoaded(bool isPrecache)
        {
        }

        public void OnRewardedVideoFailedToLoad()
        {
        }

        public void OnRewardedVideoShowFailed()
        {
        }

        public void OnRewardedVideoShown()
        {
        }

        public void OnRewardedVideoFinished(double amount, string currency)
        {
        }

        public void OnRewardedVideoClosed(bool finished)
        {
        }

        public void OnRewardedVideoExpired()
        {
        }

        public void OnRewardedVideoClicked()
        {
        }
    }
}