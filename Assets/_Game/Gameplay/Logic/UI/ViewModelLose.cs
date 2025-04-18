using System;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using MVVM;
using R3;
using Zenject;

namespace _Game.Gameplay.Logic.UI
{
    public class ViewModelLose : IInitializable, IDisposable
    {
        public event Action OnLose;
        
        [Data("Points")]
        public readonly ReactiveProperty<string> Points = new();
        
        private readonly ShipAbstract _ship;
        private readonly SceneHandler _sceneHandler;
        private readonly ScoreCounter _scoreCounter;

        public ViewModelLose(ShipAbstract ship, SceneHandler sceneHandler, ScoreCounter scoreCounter)
        {
            _ship = ship;
            _sceneHandler = sceneHandler;
            _scoreCounter = scoreCounter;
        }

        public void Initialize()
        {
            _ship.OnShipDestroyed += Show;
        }

        public void Dispose()
        {
            _ship.OnShipDestroyed -= Show;
        }

        private void Show()
        {
            Points.Value = $"You points :{_scoreCounter.GetCurrentSessionScore()} ";
            OnLose?.Invoke();
        }

        [Method("Restart")]
        public void Restart()
        {
            _sceneHandler.Restart();
        }

        [Method("Quit")]
        public void Quit()
        {
            _sceneHandler.SceneTransition("MainMenu");
        }
    }
}