using System;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using _Game.Logic.MetaService.SceneTransitorService;
using R3;
using Zenject;

namespace _Game.Gameplay.Logic.UI.LoseUI
{
    public class ViewModelLose : IInitializable, IDisposable
    {
        public ReactiveProperty<string> Points { get; private set; } = new();
        public ReactiveCommand RestartCommand { get; private set; } = new();
        public ReactiveCommand QuitCommand { get; private set; } = new();

        private readonly GameTimeHandler _gameTimeHandler;
        private readonly ShipAbstract _ship;
        private readonly SceneTransitioner _sceneTransitioner;
        private readonly ScoreCounter _scoreCounter;

        public ViewModelLose(GameTimeHandler gameTimeHandler, ShipAbstract ship, SceneTransitioner sceneTransitioner,
            ScoreCounter scoreCounter)
        {
            _gameTimeHandler = gameTimeHandler;
            _ship = ship;
            _sceneTransitioner = sceneTransitioner;
            _scoreCounter = scoreCounter;
        }

        public void Initialize()
        {
            RestartCommand.Subscribe(_ => Restart());
            QuitCommand.Subscribe(_ => Quit());
            
            _ship.OnLoseLastLife += ShowPoints;
            _ship.OnLoseLastLife += _gameTimeHandler.LoseGame;
        }


        public void Dispose()
        {
            RestartCommand?.Dispose();
            QuitCommand?.Dispose();
            Points?.Dispose();
            
            _ship.OnLoseLastLife -= ShowPoints;
            _ship.OnLoseLastLife -= _gameTimeHandler.LoseGame;
        }

        private void ShowPoints()
        {
            Points.Value = $"You points :{_scoreCounter.CurrentSessionScore} ";
        }

        private void Restart()
        {
            _sceneTransitioner.RestartGameplay();
        }

        private void Quit()
        {
            _sceneTransitioner.LoadMainMenu();
        }
    }
}