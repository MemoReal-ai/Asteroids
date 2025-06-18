using System;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.SaveAndLoadHandler;
using _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler;
using R3;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.UI.Loader
{
    public class LoaderViewModel : IInitializable, IDisposable
    {
        public event Action OnShowPopup;
        public ReactiveProperty<DateTime> CloudDataTime { get; private set; } = new();
        public ReactiveProperty<string> CloudScoreText { get; private set; } = new();
        public ReactiveProperty<DateTime> LocalDataTime { get; private set; } = new();
        public ReactiveProperty<string> LocalScoreText { get; private set; } = new();
        public ReactiveCommand ChoiceLocalSaveCommand { get; private set; } = new();
        public ReactiveCommand ChoiceCloudSaveCommand { get; private set; } = new();

        private readonly DataSyncManager _dataSyncManager;

        public LoaderViewModel(DataSyncManager dataSyncManager)
        {
            _dataSyncManager = dataSyncManager;
        }

        public async void Initialize()
        {
            try
            {
                _dataSyncManager.OnNotValidData += ShowPopup;

                await _dataSyncManager.CheckLoadedData();
              
                var localData = _dataSyncManager.GetLocalSaveData();
                SetSubscribe(ChoiceLocalSaveCommand, LocalScoreText, LocalDataTime, localData);
                
                var cloudData = _dataSyncManager.GetCloudSaveData();
                SetSubscribe(ChoiceCloudSaveCommand, CloudScoreText, CloudDataTime, cloudData ?? new Data());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Dispose()
        {
            _dataSyncManager.OnNotValidData -= ShowPopup;
            UnsubscribeReactive();
        }

        private void SetSubscribe(ReactiveCommand buttonChoice, ReactiveProperty<string> hightScore,
            ReactiveProperty<DateTime> saveTime, Data data)
        {
            buttonChoice.Subscribe(x => _dataSyncManager.SetData(data));
            hightScore.Value = $" High Score {data.HightScore.ToString()}";
            saveTime.Value = data.SaveTime;
        }

        private void UnsubscribeReactive()
        {
            ChoiceLocalSaveCommand?.Dispose();
            ChoiceCloudSaveCommand?.Dispose();
            CloudScoreText?.Dispose();
            LocalScoreText?.Dispose();
            CloudDataTime?.Dispose();
            LocalDataTime?.Dispose();
        }

        private void ShowPopup()
        {
            OnShowPopup?.Invoke();
        }
    }
}