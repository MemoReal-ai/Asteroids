using _Game.Logic.MetaService.AdsServiceUnity;

namespace _Game.Logic.MetaService.AppodealAds
{
    public class AppodealInterstitialAds : IInterstitialAds
    {
        private readonly IAdsService _adsService;

        public AppodealInterstitialAds(IAdsService adsService)
        {
            _adsService = adsService;
        }

        public void ShowAds()
        {
            _adsService.ShowPassiveAds(null);
        }
    }
}