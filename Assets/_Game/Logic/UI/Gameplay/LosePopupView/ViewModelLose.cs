using System;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using _Game.Logic.MetaService.SceneTransitionerService;
using R3;
using Zenject;

namespace _Game.Logic.UI.Gameplay.LosePopupView
{
    public class ViewModelLose : IInitializable, IDisposable
    {
        private readonly LoseView _loseView;
        private readonly GameTimeHandler _gameTimeHandler;
        private readonly ShipAbstract _ship;
        private readonly SceneTransitioner _sceneTransitioner;
        private readonly ScoreCounter _scoreCounter;

        private ReactiveProperty<string> Points { get; } = new();

        public ViewModelLose(GameTimeHandler gameTimeHandler, ShipAbstract ship, SceneTransitioner sceneTransitioner,
            ScoreCounter scoreCounter, LoseView loseView)
        {
            _gameTimeHandler = gameTimeHandler;
            _ship = ship;
            _sceneTransitioner = sceneTransitioner;
            _scoreCounter = scoreCounter;
            _loseView = loseView;
        }

        public void Initialize()
        {
            _ship.OnLoseLastLife += _loseView.Show;
            _ship.OnLoseLastLife += ShowPoints;
            _ship.OnLoseLastLife += _gameTimeHandler.LoseGame;
            Bind();
        }

        public void Dispose()
        {
            Points?.Dispose();

            _ship.OnLoseLastLife -= _loseView.Show;
            _ship.OnLoseLastLife -= ShowPoints;
            _ship.OnLoseLastLife -= _gameTimeHandler.LoseGame;
        }

        private void Bind()
        {
            _loseView.RestartButton.OnClickAsObservable().Subscribe(_sceneTransitioner.RestartGameCommand.Execute)
                .AddTo(_loseView);

            _loseView.QuitButton.OnClickAsObservable().Subscribe(_sceneTransitioner.MainMenuTransitionCommand.Execute)
                .AddTo(_loseView);

            Points.Subscribe(x => _loseView.ShowPoints(x));
        }

        private void ShowPoints()
        {
            Points.Value = $"You points :{_scoreCounter.CurrentSessionScore} ";
        }
    }
}