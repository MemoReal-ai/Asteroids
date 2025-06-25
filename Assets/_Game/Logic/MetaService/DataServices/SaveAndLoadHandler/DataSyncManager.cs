using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Logic.MetaService.AuthenticatorService;
using _Game.Logic.MetaService.DataServices.SaveAndLoadHandler;
using _Game.Logic.MetaService.Purchasing_Service;
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

        private PlayerProgressData _cloudPlayerProgressData;
        private PlayerProgressData _localPlayerProgressData;
        public PlayerProgressData PlayerProgressData { get; private set; }

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

        public async void SetData(PlayerProgressData playerProgressData)
        {
            try
            {
                playerProgressData ??= await _savers[LOCAL_INDEX_SAVER_LIST].LoadData();
                PlayerProgressData = playerProgressData;
                _purchasingService.SetFlagPurchasingAdsSkip(PlayerProgressData.PurchasingSkipAds);
                _setValidSave.TrySetResult();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public PlayerProgressData GetLocalSaveData()
        {
            return _localPlayerProgressData;
        }

        public PlayerProgressData GetCloudSaveData()
        {
            return _cloudPlayerProgressData;
        }

        public async UniTask WaitSetValidData()
        {
            await _setValidSave.Task;
        }

        private async UniTask<bool> CheckValidData()
        {
            _localPlayerProgressData = await _savers[LOCAL_INDEX_SAVER_LIST].LoadData();
            _cloudPlayerProgressData = await _savers[CLOUD_INDEX_SAVER_LIST].LoadData();
            _initializationData.TrySetResult();

            if (_cloudPlayerProgressData == null)
            {
                return true;
            }

            if (Mathf.Abs(_cloudPlayerProgressData.SaveTime.Date.Ticks - _localPlayerProgressData.SaveTime.Date.Ticks) < TRESHOLD_DIFFERENCE_TICK)
            {
                return true;
            }

            OnNotValidData?.Invoke();
            return false;
        }

        private void SaveData(ISaver saver)
        {
            PlayerProgressData.CurrentScore = _scoreCounter.CurrentSessionScore;
            PlayerProgressData.ChangeScore();
            PlayerProgressData.PurchasingSkipAds = _purchasingService.HasPurchasingAdsSkip();
            PlayerProgressData.SaveTime = DateTime.UtcNow;
            saver.SaveData(PlayerProgressData);
        }
    }
}