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

        private readonly ReactiveCommand GameplayTransitionCommand = new();
        private readonly ReactiveCommand ExitGameCommand = new();

        public MainMenuViewModel(SceneTransitioner sceneTransitioner, ViewMainMenu viewMainMenu)
        {
            _sceneTransitioner = sceneTransitioner;
            _viewMainMenu = viewMainMenu;
        }

        public void Initialize()
        {
            SubscribeProperty();
            Bind();
        }

        public void Dispose()
        {
            GameplayTransitionCommand?.Dispose();
            ExitGameCommand?.Dispose();
        }

        private void SubscribeProperty()
        {
            GameplayTransitionCommand.Subscribe(_ => _sceneTransitioner.LoadGameplayScene());
            ExitGameCommand.Subscribe(_ => _sceneTransitioner.Quit());
        }

        private void Bind()
        {
            _viewMainMenu.StartGameButton
                .OnClickAsObservable()
                .Subscribe(GameplayTransitionCommand.Execute)
                .AddTo(_viewMainMenu);

            _viewMainMenu.ExitGameButton
                .OnClickAsObservable()
                .Subscribe(ExitGameCommand.Execute)
                .AddTo(_viewMainMenu);
        }
    }
}