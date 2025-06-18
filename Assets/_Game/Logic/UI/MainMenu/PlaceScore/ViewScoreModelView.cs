using System;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.SaveAndLoadHandler;
using _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler;
using Cysharp.Threading.Tasks;
using Zenject;
using R3;

namespace _Game.MainMenu.Logic.UI
{
    public class ViewScoreModelView : IInitializable, IDisposable
    {
        public ReactiveProperty<string> ScoreLastSession { get; private set; } = new();
        public ReactiveProperty<string> HighScore { get; private set; } = new();

        private readonly UniTaskCompletionSource _initializeTaskCompletionSource = new();
        private readonly DataSyncManager _dataSyncManager;

        public ViewScoreModelView(DataSyncManager dataSyncManager)
        {
            _dataSyncManager = dataSyncManager;
        }

        public async void Initialize()
        {
            try
            {
                await _dataSyncManager.CheckLoadedData();
                ScoreLastSession.Value = _dataSyncManager.Data.CurrentScore.ToString();
                HighScore.Value = _dataSyncManager.Data.HightScore.ToString();
                _initializeTaskCompletionSource.TrySetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public void Dispose()
        {
            ScoreLastSession.Dispose();
            HighScore.Dispose();
        }
    }
}