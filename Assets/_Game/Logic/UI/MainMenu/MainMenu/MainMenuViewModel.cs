using System;
using _Game.Logic.MetaService.SceneTransitionerService;
using _Game.MainMenu.Logic.UI;
using R3;
using Zenject;

namespace _Game.Logic.UI.MainMenu.MainMenu
{
    public class MainMenuViewModel : IInitializable, IDisposable
    {
        private readonly SceneTransitioner _sceneTransitioner;
        private readonly ViewMainMenu _viewMainMenu;
        private ReactiveCommand GameplayTransitionCommand { get; } = new();
        private ReactiveCommand ExitCommand { get; } = new();

        public MainMenuViewModel(SceneTransitioner sceneTransitioner, ViewMainMenu viewMainMenu)
        {
            _sceneTransitioner = sceneTransitioner;
            _viewMainMenu = viewMainMenu;
        }

        public void Initialize()
        {
            GameplayTransitionCommand.Subscribe(x => OnGoToGameplayScene());
            ExitCommand.Subscribe(x => OnExitGameplayScene());
            Bind();
        }

        public void Dispose()
        {
            GameplayTransitionCommand?.Dispose();
            ExitCommand?.Dispose();
        }

        private void Bind()
        {
            _viewMainMenu.StartGameButton
                .OnClickAsObservable()
                .Subscribe(GameplayTransitionCommand.Execute)
                .AddTo(_viewMainMenu);

            _viewMainMenu.ExitGameButton
                .OnClickAsObservable()
                .Subscribe(ExitCommand.Execute)
                .AddTo(_viewMainMenu);
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