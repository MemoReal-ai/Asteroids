using System;
using _Game.Gameplay.Logic.Service;
using Zenject;
using R3;

namespace _Game.MainMenu.Logic.UI
{
    public class ViewScoreModelView : IInitializable, IDisposable
    {
        public ReactiveProperty<string> ScoreLastSession { get; private set; } = new();
        public ReactiveProperty<string> HighScore { get; private set; } = new();

        private readonly DataHandler _dataHandler;

        public ViewScoreModelView(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public void Initialize()
        {
            ScoreLastSession.Value = _dataHandler.Data.CurrentScore.ToString();
            HighScore.Value = _dataHandler.Data.HightScore.ToString();
        }

        public void Dispose()
        {
            ScoreLastSession.Dispose();
            HighScore.Dispose();
        }
    }
}