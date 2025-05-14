using System;
using _Game.Gameplay.Logic.Infrastructure;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Features
{
    public class ScoreCounter : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private int _maxScore;
        private SceneHandler _sceneHandler;
        public int CurrentSessionScore { get; private set; } = 0;

        public ScoreCounter(SignalBus signal,SceneHandler sceneHandler)
        {
            _signalBus = signal;
            _sceneHandler = sceneHandler;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EnemyDiedSignal>(CalculateScore);
            _sceneHandler.OnSceneRestart+=ResetScore;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyDiedSignal>(CalculateScore);
            _sceneHandler.OnSceneRestart-=ResetScore;
        }
        

        private void CalculateScore(EnemyDiedSignal signal)
        {
            CurrentSessionScore += signal.Reward;
            
        }

        public void ResetScore()
        {
            CurrentSessionScore = 0;
        }
        
        
    }
}