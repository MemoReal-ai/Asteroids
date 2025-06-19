using System;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using _Game.Logic.MetaService.SceneTransitionerService;
using R3;
using Zenject;

namespace _Game.Logic.UI.Gameplay.LoseView
{
    public class ViewModelLose : IInitializable, IDisposable
    {
        private readonly _Game.Gameplay.Logic.UI.LoseView _loseView;
        private readonly GameTimeHandler _gameTimeHandler;
        private readonly ShipAbstract _ship;
        private readonly SceneTransitioner _sceneTransitioner;
        private readonly ScoreCounter _scoreCounter;

        private ReactiveProperty<string> Points { get; set; } = new();
        private ReactiveCommand RestartCommand { get; set; } = new();
        private ReactiveCommand QuitCommand { get; set; } = new();


        public ViewModelLose(GameTimeHandler gameTimeHandler, ShipAbstract ship, SceneTransitioner sceneTransitioner,
            ScoreCounter scoreCounter, _Game.Gameplay.Logic.UI.LoseView loseView)
        {
            _gameTimeHandler = gameTimeHandler;
            _ship = ship;
            _sceneTransitioner = sceneTransitioner;
            _scoreCounter = scoreCounter;
            _loseView = loseView;
        }

        public void Initialize()
        {
            RestartCommand.Subscribe(_ => Restart());
            QuitCommand.Subscribe(_ => Quit());

            _ship.OnLoseLastLife += _loseView.Show;
            _ship.OnLoseLastLife += ShowPoints;
            _ship.OnLoseLastLife += _gameTimeHandler.LoseGame;
            Bind();
        }


        public void Dispose()
        {
            RestartCommand?.Dispose();
            QuitCommand?.Dispose();
            Points?.Dispose();
            
            _ship.OnLoseLastLife -= _loseView.Show;
            _ship.OnLoseLastLife -= ShowPoints;
            _ship.OnLoseLastLife -= _gameTimeHandler.LoseGame;
        }

        private void Bind()
        {
            _loseView.RestartButton.
                OnClickAsObservable().
                Subscribe(RestartCommand.Execute).
                AddTo(_loseView);
            
            _loseView.QuitButton.
                OnClickAsObservable().
                Subscribe(QuitCommand.Execute).
                AddTo(_loseView);

            Points.Subscribe(x => _loseView.ShowPoints(x));
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