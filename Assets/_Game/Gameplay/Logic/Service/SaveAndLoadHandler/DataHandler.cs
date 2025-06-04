using System;
using System.Threading.Tasks;
using _Game.AuthenticatorService;
using _Game.Gameplay.Logic.Features;
using _Game.Purchasing_Service;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Service.SaveAndLoadHandler
{
    public class DataHandler : IInitializable, IDisposable
    {
        public event Action OnNotValidData;

        private readonly ILocalSaver _localSaver;
        private readonly ICloudSaver _cloudSaver;
        private readonly ScoreCounter _scoreCounter;
        private readonly IPurchasingService _purchasingService;
        private readonly UniTaskCompletionSource _initializationData = new UniTaskCompletionSource();
        private readonly IAuthenticatorService _authenticatorService;

        private Data _cloudData;
        private Data _localData;
        public Data Data { get; private set; }

        public DataHandler(ILocalSaver localSaver, ScoreCounter scoreCounter, IPurchasingService purchasingService,
            ICloudSaver cloudSaver, IAuthenticatorService authenticatorService)
        {
            _localSaver = localSaver;
            _scoreCounter = scoreCounter;
            _purchasingService = purchasingService;
            _cloudSaver = cloudSaver;
            _authenticatorService = authenticatorService;
        }

        public async void Initialize()
        {
            await _authenticatorService.WaitSignIn();
            try
            {
                if (await CheckValidData())
                {
                    var validData = await _cloudSaver.LoadDataCloud();
                    SetData(validData);
                }
                else
                {
                    OnNotValidData?.Invoke();
                    Debug.Log("No valid data");
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void Dispose()
        {
            LocalSaveData();
            CloudSaveData();
        }

        private async Task<bool> CheckValidData()
        {
            _localData = _localSaver.LoadData();
            _cloudData = await _cloudSaver.LoadDataCloud();
            _initializationData.TrySetResult();
            if (_cloudData == null || _cloudData.SaveTime == _localData.SaveTime)
            {
                return true;
            }

            return false;
        }

        public void LocalSaveData()
        {
            SaveData(_localSaver);
        }

        public void CloudSaveData()
        {
            SaveData(_cloudSaver);
        }

        public async UniTask CheckLoadedData()
        {
            await _initializationData.Task;
        }

        public void SetData(Data data)
        {
            if (data == null)
            {
                data = _localSaver.LoadData();
            }

            Data = data;
            _purchasingService.SetFlagPurchasingAdsSkip(Data.PurchasingSkipAds);
        }

        public Data GetLocalSaveData()
        {
            return _localData;
        }

        public Data GetCloudSaveData()
        {
            return _cloudData;
        }

        private void SaveData(ISaver saver)
        {
            Data.CurrentScore = _scoreCounter.CurrentSessionScore;
            Data.ChangeScore();
            Data.PurchasingSkipAds = _purchasingService.HasPurchasingAdsSkip();
            Data.SaveTime = DateTime.UtcNow;
            saver.SaveData(Data);
        }
    }
}