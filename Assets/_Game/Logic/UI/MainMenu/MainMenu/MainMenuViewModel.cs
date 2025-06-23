using System;
using _Game.Logic.MetaService.SceneTransitionerService;
using _Game.MainMenu.Logic.UI;
using R3;
using Zenject;

namespace _Game.Logic.UI.MainMenu.MainMenu
{
    public class MainMenuViewModel : IInitializable
    {
        private readonly SceneTransitioner _sceneTransitioner;
        private readonly ViewMainMenu _viewMainMenu;
        public MainMenuViewModel(SceneTransitioner sceneTransitioner, ViewMainMenu viewMainMenu)
        {
            _sceneTransitioner = sceneTransitioner;
            _viewMainMenu = viewMainMenu;
        }

        public void Initialize()
        {
            Bind();
        }
        
        private void Bind()
        {
            _viewMainMenu.StartGameButton
                .OnClickAsObservable()
                .Subscribe(_sceneTransitioner.GameplayTransitionCommand.Execute)
                .AddTo(_viewMainMenu);

            _viewMainMenu.ExitGameButton
                .OnClickAsObservable()
                .Subscribe(_sceneTransitioner.ExitGameCommand.Execute)
                .AddTo(_viewMainMenu);
        }
    }
}