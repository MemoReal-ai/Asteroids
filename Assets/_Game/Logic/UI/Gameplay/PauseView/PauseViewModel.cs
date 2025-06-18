using System;
using _Game.Gameplay.Logic.Service;
using _Game.Logic.Gameplay.Service.Input;
using _Game.Logic.MetaService.SceneTransitorService;
using R3;
using Zenject;

namespace _Game.Gameplay.Logic.UI
{
    public class PauseViewModel : IInitializable, IDisposable
    {
        private readonly SceneTransitioner _sceneTransitioner;
        private readonly IInput _input;
        public ReactiveCommand ExitCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand ResumeCommand { get; private set; } = new ReactiveCommand();

        public PauseViewModel(SceneTransitioner sceneTransitioner, IInput input)
        {
            _sceneTransitioner = sceneTransitioner;
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
            _sceneTransitioner.LoadMainMenu();
        }
    }
}