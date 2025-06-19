using System;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.UI;
using _Game.Logic.Gameplay.Service.Input;
using _Game.Logic.MetaService.SceneTransitionerService;
using R3;
using Zenject;

namespace _Game.Logic.UI.Gameplay.Pause
{
    public class PauseViewModel : IInitializable, IDisposable
    {
        private readonly SceneTransitioner _sceneTransitioner;
        private readonly IInput _input;
        private readonly PauseView _pauseView;
        private readonly GameTimeHandler _gameTimeHandler;

        private ReactiveCommand ExitCommand { get; } = new();
        private ReactiveCommand ResumeCommand { get; } = new();

        public PauseViewModel(SceneTransitioner sceneTransitioner, IInput input,
            PauseView pauseView, GameTimeHandler gameTimeHandler)
        {
            _sceneTransitioner = sceneTransitioner;
            _input = input;
            _pauseView = pauseView;
            _gameTimeHandler = gameTimeHandler;
        }

        public void Initialize()
        {
            _gameTimeHandler.OnPaused += _pauseView.Show;
            _gameTimeHandler.OnResume += _pauseView.Hide;
            ExitCommand.Subscribe(_ => GoToMainMenu());
            ResumeCommand.Subscribe(_ => ResumeGame());
            Bind();
        }

        public void Dispose()
        {
            ExitCommand?.Dispose();
            ResumeCommand?.Dispose();

            _gameTimeHandler.OnPaused -= _pauseView.Show;
            _gameTimeHandler.OnResume -= _pauseView.Hide;
        }

        private void Bind()
        {
            _pauseView.ExitButton.OnClickAsObservable().Subscribe(ExitCommand.Execute).AddTo(_pauseView);
            _pauseView.ResumeButton.OnClickAsObservable().Subscribe(ResumeCommand.Execute).AddTo(_pauseView);
        }

        private void ResumeGame()
        {
            _input.PressedResume();
        }

        private void GoToMainMenu()
        {
            _sceneTransitioner.LoadMainMenu();
        }
    }
}