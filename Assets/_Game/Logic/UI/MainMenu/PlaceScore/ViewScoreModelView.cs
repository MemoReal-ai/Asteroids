using System;
using _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler;
using _Game.MainMenu.Logic.UI;
using R3;
using Zenject;

namespace _Game.Logic.UI.MainMenu.PlaceScore
{
    public class ViewScoreModelView : IInitializable, IDisposable
    {
        private readonly DataSyncManager _dataSyncManager;
        private readonly ViewScore _viewScore;

        private ReactiveProperty<string> ScoreLastSession { get; set; } = new();
        private ReactiveProperty<string> HighScore { get; set; } = new();

        public ViewScoreModelView(DataSyncManager dataSyncManager, ViewScore viewScore)
        {
            _dataSyncManager = dataSyncManager;
            _viewScore = viewScore;
        }

        public async void Initialize()
        {
            try
            {
                await _dataSyncManager.WaitSetValidData();
                ScoreLastSession.Value = _dataSyncManager.Data.CurrentScore.ToString();
                HighScore.Value = _dataSyncManager.Data.HightScore.ToString();
                Bind();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Bind()
        {
            ScoreLastSession.Subscribe(x => _viewScore.SetScoreLastSession(x));
            HighScore.Subscribe(x => _viewScore.SetHighScore(x));
        }

        public void Dispose()
        {
            ScoreLastSession.Dispose();
            HighScore.Dispose();
        }
    }
}