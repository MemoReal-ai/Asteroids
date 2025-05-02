using System;
using _Game.Gameplay.Logic.Service;
using R3;
using Zenject;

namespace _Game.Gameplay.Logic.UI
{
    public class PauseViewModel : IInitializable, IDisposable
    {
        private readonly SceneHandler _sceneHandler;
        private readonly IInput _input;
        public ReactiveCommand ExitCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand ResumeCommand { get; private set; } = new ReactiveCommand();

        public PauseViewModel(SceneHandler sceneHandler, IInput input)
        {
            _sceneHandler = sceneHandler;
            _input = input;
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
            _input.PressedResume();
        }

        private void GoToMainMenu()
        {
            _sceneHandler.SceneTransition("MainMenu");
        }
    }
}