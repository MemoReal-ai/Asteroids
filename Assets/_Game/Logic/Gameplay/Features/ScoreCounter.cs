using System;
using _Game.Gameplay.Logic.Service;
using _Game.Logic.Gameplay.Features;
using Zenject;

namespace _Game.Gameplay.Logic.Features
{
    public class ScoreCounter : IInitializable, IDisposable, IScoreCounter
    {
        private int _maxScore;
        private readonly SceneHandler _sceneHandler;
        public int CurrentSessionScore { get; private set; } = 0;

        public ScoreCounter(SceneHandler sceneHandler)
        {
            _sceneHandler = sceneHandler;
        }

        public void Initialize()
        {
            _sceneHandler.OnSceneRestart += ResetScore;
        }

        public void Dispose()
        {
            _sceneHandler.OnSceneRestart -= ResetScore;
        }

        private void ResetScore()
        {
            CurrentSessionScore = 0;
        }

        public void IncreaseScore(int reward)
        {
            CurrentSessionScore += reward;
        }
    }
}