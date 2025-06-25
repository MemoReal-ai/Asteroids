using System;
using _Game.Gameplay.Logic.Service;
using _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler;
using _Game.Logic.MetaService.DataServices.SaveAndLoadHandler;
using _Game.MainMenu.Logic.UI;
using R3;
using Zenject;

namespace _Game.Logic.UI.MainMenu.Loader
{
    public class LoaderChoisenDataViewModel : IInitializable, IDisposable
    {
        private readonly DataSyncManager _dataSyncManager;
        private readonly LoaderView _loaderView;

        private ReactiveProperty<DateTime> CloudDataTime { get; set; } = new();
        private ReactiveProperty<string> CloudScoreText { get; set; } = new();
        private ReactiveProperty<DateTime> LocalDataTime { get; set; } = new();
        private ReactiveProperty<string> LocalScoreText { get; set; } = new();
        private ReactiveCommand ChoiceLocalSaveCommand { get; set; } = new();
        private ReactiveCommand ChoiceCloudSaveCommand { get; set; } = new();

        public LoaderChoisenDataViewModel(DataSyncManager dataSyncManager, LoaderView loaderView)
        {
            _dataSyncManager = dataSyncManager;
            _loaderView = loaderView;
        }

        public async void Initialize()
        {
            try
            {
                _dataSyncManager.OnNotValidData += _loaderView.Show;

                await _dataSyncManager.CheckLoadedData();

                var localData = _dataSyncManager.GetLocalSaveData();
                SetSubscribe(ChoiceLocalSaveCommand, LocalScoreText, LocalDataTime, localData);

                var cloudData = _dataSyncManager.GetCloudSaveData();
                SetSubscribe(ChoiceCloudSaveCommand, CloudScoreText, CloudDataTime, cloudData ?? new PlayerProgressData());

                Bind();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Dispose()
        {
            _dataSyncManager.OnNotValidData -= _loaderView.Show;
            UnsubscribeReactive();
        }

        private void SetSubscribe(ReactiveCommand buttonChoice, ReactiveProperty<string> hightScore,
            ReactiveProperty<DateTime> saveTime, PlayerProgressData playerProgressData)
        {
            buttonChoice.Subscribe(x =>
            {
                _dataSyncManager.SetData(playerProgressData);
                Hide();
            });
            hightScore.Value = $" High Score {playerProgressData.HightScore.ToString()}";
            saveTime.Value = playerProgressData.SaveTime;
        }

        private void Hide()
        {
            _loaderView.Hide();
        }

        private void Bind()
        {
            _loaderView.CloudSaveGroup.LoadButton.OnClickAsObservable().Subscribe(ChoiceCloudSaveCommand.Execute)
                .AddTo(_loaderView);
            CloudScoreText.Subscribe(x => _loaderView.SetText(x, _loaderView.CloudSaveGroup.ScoreDataText))
                .AddTo(_loaderView);
            CloudDataTime.Subscribe(x => _loaderView.SetText(x.ToString(), _loaderView.CloudSaveGroup.TimeDataText))
                .AddTo(_loaderView);
            
            _loaderView.LocalSaveGroup.LoadButton.OnClickAsObservable().Subscribe(ChoiceLocalSaveCommand.Execute)
                .AddTo(_loaderView);
            LocalScoreText.Subscribe(x => _loaderView.SetText(x.ToString(), _loaderView.LocalSaveGroup.ScoreDataText))
                .AddTo(_loaderView);
            LocalDataTime.Subscribe(x => _loaderView.SetText(x.ToString(), _loaderView.LocalSaveGroup.TimeDataText))
                .AddTo(_loaderView);
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
    }
}