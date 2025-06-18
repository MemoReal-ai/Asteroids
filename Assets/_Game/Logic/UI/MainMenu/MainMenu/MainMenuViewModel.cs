using System;
using _Game.Gameplay.Logic.Service;
using _Game.Logic.MetaService.SceneTransitorService;
using Zenject;
using R3;

namespace _Game.MainMenu.Logic.UI
{
    public class MainMenuViewModel : IInitializable, IDisposable
    {
        private readonly SceneTransitioner _sceneTransitioner;
        public ReactiveCommand GameplayTransitionCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand ExitCommand { get; private set; } = new ReactiveCommand();

        public MainMenuViewModel(SceneTransitioner sceneTransitioner)
        {
            _sceneTransitioner = sceneTransitioner;
        }


        public void Initialize()
        {
            GameplayTransitionCommand.Subscribe(_ => OnGoToGameplayScene());
            ExitCommand.Subscribe(_=>OnExitGameplayScene());
        }

        public void Dispose()
        {
            GameplayTransitionCommand?.Dispose();
            ExitCommand?.Dispose();
        }

        private void OnGoToGameplayScene()
        {
            _sceneTransitioner.LoadGameplayScene();
        }

        private void OnExitGameplayScene()
        {
            _sceneTransitioner.Quit();
        }
    }
}