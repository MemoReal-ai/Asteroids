using System;
using _Game.Gameplay.Logic.Service;
using R3;
using Zenject;

namespace _Game.Gameplay.Logic.UI
{
    public class PauseViewModel : IInitializable, IDisposable
    {
        private readonly SceneHandler _sceneHandler;
        private readonly GameTimeHandler _gameTimeHandler;
        public ReactiveCommand ExitCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand ResumeCommand { get; private set; } = new ReactiveCommand();

        public PauseViewModel(SceneHandler sceneHandler, GameTimeHandler gameTimeHandler)
        {
            _sceneHandler = sceneHandler;
            _gameTimeHandler = gameTimeHandler;
        }

        public void Initialize()
        {
            ExitCommand.Subscribe(_ => GoToMainMenu());
            ResumeCommand.Subscribe(_ => ResumeGame());
        }

        public void Dispose()
        {
            ExitCommand?.Dispose();
            ResumeCommand?.Dispose();
        }

        private void ResumeGame()
        {
            _gameTimeHandler.Unpause();
        }

        private void GoToMainMenu()
        {
            _sceneHandler.SceneTransition("MainMenu");
        }
    }
}