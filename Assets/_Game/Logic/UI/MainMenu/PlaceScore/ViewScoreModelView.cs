using System;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.SaveAndLoadHandler;
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
        private readonly DataHandler _dataHandler;

        public ViewScoreModelView(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public async void Initialize()
        {
            try
            {
                await _dataHandler.CheckLoadedData();
                ScoreLastSession.Value = _dataHandler.Data.CurrentScore.ToString();
                HighScore.Value = _dataHandler.Data.HightScore.ToString();
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