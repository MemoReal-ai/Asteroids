using System;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service.SaveAndLoadHandler;
using _Game.Purchasing_Service;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class DataHandler : IInitializable, IDisposable
    {
        private readonly ILocalSaver _dataLocalSaver;
        private readonly ICloudSaver _cloudSaver;
        private readonly ScoreCounter _scoreCounter;
        private readonly IPurchasingService _purchasingService;

        public Data Data { get; private set; }

        public DataHandler(ILocalSaver dataLocalSaver, ScoreCounter scoreCounter, IPurchasingService purchasingService,
            ICloudSaver cloudSaver)
        {
            _dataLocalSaver = dataLocalSaver;
            _scoreCounter = scoreCounter;
            _purchasingService = purchasingService;
        }

        public void Initialize()
        {
            Data = _dataLocalSaver.LoadData();
            _purchasingService.SetFlagPurchasingAdsSkip(Data.PurchasingSkipAds);
        }

        public void Dispose()
        {
            LocalSaveData();
        }

        public void LocalSaveData()
        {
            Data.CurrentScore = _scoreCounter.CurrentSessionScore;
            Data.ChangeScore();
            Data.PurchasingSkipAds = _purchasingService.HasPurchasingAdsSkip();
            Data.SaveTime = DateTime.UtcNow;
            _dataLocalSaver.SaveData(Data);
        }

        public void CloudSaveData()
        {
        }
    }
}