using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Logic.MetaService.AuthenticatorService;
using _Game.Purchasing_Service;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler
{
    public class DataSyncManager : IInitializable, IDisposable
    {
        private const int TRESHOLD_DIFFERENCE_TICK = 10;
        private const int LOCAL_INDEX_SAVER_LIST = 0;
        private const int CLOUD_INDEX_SAVER_LIST = 1;
        public event Action OnNotValidData;

        private readonly List<ISaver> _savers;
        private readonly ScoreCounter _scoreCounter;
        private readonly IPurchasingService _purchasingService;
        private readonly UniTaskCompletionSource _initializationData = new();
        private readonly UniTaskCompletionSource _setValidSave = new();
        private readonly IAuthenticatorService _authenticatorService;

        private Data _cloudData;
        private Data _localData;
        public Data Data { get; private set; }

        public DataSyncManager(ScoreCounter scoreCounter, IPurchasingService purchasingService,
            IAuthenticatorService authenticatorService, List<ISaver> savers)
        {
            _scoreCounter = scoreCounter;
            _purchasingService = purchasingService;
            _authenticatorService = authenticatorService;
            _savers = savers;
        }

        public async void Initialize()
        {
            await _authenticatorService.WaitSignIn();
            try
            {
                if (await CheckValidData())
                {
                    var validData = await _savers[LOCAL_INDEX_SAVER_LIST].LoadData();
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
            Save();
        }

        public void Save()
        {
            foreach (var saver in _savers)
            {
                SaveData(saver);
            }
        }

        public async UniTask CheckLoadedData()
        {
            await _initializationData.Task;
        }

        public async void SetData(Data data)
        {
            try
            {
                data ??= await _savers[LOCAL_INDEX_SAVER_LIST].LoadData();
                Data = data;
                _purchasingService.SetFlagPurchasingAdsSkip(Data.PurchasingSkipAds);
                _setValidSave.TrySetResult();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public Data GetLocalSaveData()
        {
            return _localData;
        }

        public Data GetCloudSaveData()
        {
            return _cloudData;
        }

        public async UniTask WaitSetValidData()
        {
            await _setValidSave.Task;
        }

        private async UniTask<bool> CheckValidData()
        {
            _localData = await _savers[LOCAL_INDEX_SAVER_LIST].LoadData();
            _cloudData = await _savers[CLOUD_INDEX_SAVER_LIST].LoadData();
            _initializationData.TrySetResult();

            if (_cloudData == null)
            {
                return true;
            }

            if (Mathf.Abs(_cloudData.SaveTime.Date.Ticks - _localData.SaveTime.Date.Ticks) < TRESHOLD_DIFFERENCE_TICK)
            {
                return true;
            }

            OnNotValidData?.Invoke();
            return false;
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