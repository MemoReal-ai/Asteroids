using System;
using _Game.Gameplay.Logic.Features;
using _Game.Purchasing_Service;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class DataHandler : IInitializable, IDisposable
    {
        private readonly ISaver _dataSaver;
        private readonly ScoreCounter _scoreCounter;
        private readonly IPurchasingService _purchasingService;
        
        public Data Data { get; private set; }

        public DataHandler(ISaver dataSaver, ScoreCounter scoreCounter, IPurchasingService purchasingService)
        {
            _dataSaver = dataSaver;
            _scoreCounter = scoreCounter;
            _purchasingService = purchasingService;
        }

        public void Initialize()
        {
            Data = _dataSaver.LoadData();
            _purchasingService.SetFlagPurchasingAdsSkip(Data.PurchasingSkipAds);
        }

        public void Dispose()
        {
            SaveData();
        }

        public void SaveData()
        {
            Data.CurrentScore = _scoreCounter.CurrentSessionScore;
            Data.ChangeScore();
            Data.PurchasingSkipAds = _purchasingService.HasPurchasingAdsSkip();
            _dataSaver.SaveData(Data);
        }
    }
}