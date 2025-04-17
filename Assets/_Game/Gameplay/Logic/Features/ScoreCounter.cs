using System;
using _Game.Gameplay.Logic.Infrastructure;
using UnityEngine;
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
            _signalBus.Subscribe<EnemyDiedSignal>(CalculateScore);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyDiedSignal>(CalculateScore);
        }

        public int GetCurrentSessionScore()
        {
            return _currentSessionScore;
        }

        private void CalculateScore(EnemyDiedSignal signal)
        {
            _currentSessionScore += signal.Reward;
        }
        
        
    }
}