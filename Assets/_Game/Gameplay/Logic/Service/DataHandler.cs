using System;
using _Game.Gameplay.Logic.Features;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class DataHandler : IInitializable, IDisposable
    {
        private readonly ISaver _dataSaver;
        private readonly ScoreCounter _scoreCounter;
        
        public Data Data { get; private set; }

        public DataHandler(ISaver dataSaver, ScoreCounter scoreCounter)
        {
            _dataSaver = dataSaver;
            _scoreCounter = scoreCounter;
        }

        public void Initialize()
        {
            Data = _dataSaver.LoadData();
        }

        public void Dispose()
        {
            SaveData();
        }

        public void SaveData()
        {
            Data.CurrentScore = _scoreCounter.CurrentSessionScore;
            Data.ChangeScore();
            _dataSaver.SaveData(Data);
        }
    }
}