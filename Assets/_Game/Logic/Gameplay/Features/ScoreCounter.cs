using System;
using _Game.Gameplay.Logic.Service;
using _Game.Logic.Gameplay.Features;
using _Game.Logic.MetaService.SceneTransitorService;
using Zenject;

namespace _Game.Gameplay.Logic.Features
{
    public class ScoreCounter : IInitializable, IDisposable, IScoreCounter
    {
        private int _maxScore;
        private readonly SceneTransitioner _sceneTransitioner;
        public int CurrentSessionScore { get; private set; } = 0;

        public ScoreCounter(SceneTransitioner sceneTransitioner)
        {
            _sceneTransitioner = sceneTransitioner;
        }

        public void Initialize()
        {
            _sceneTransitioner.OnSceneRestart += ResetScore;
        }

        public void Dispose()
        {
            _sceneTransitioner.OnSceneRestart -= ResetScore;
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