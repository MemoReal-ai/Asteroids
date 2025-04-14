using System;
using Zenject;

namespace _Game.Gameplay.Logic.Features
{
    public class ScoreCounter : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private int _currentSessionScore;
        private int _maxScore;

        public ScoreCounter(SignalBus signal)
        {
            _signalBus = signal;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public int GetCurrentSessionScore()
        {
            return _currentSessionScore;
        }

        private void CalculateScore(int value)
        {
            _currentSessionScore += value;
        }
    }
}