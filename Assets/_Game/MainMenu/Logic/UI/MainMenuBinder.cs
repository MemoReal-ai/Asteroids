using System;
using Zenject;
using R3;

namespace _Game.MainMenu.Logic.UI
{
    public class MainMenuBinder : IInitializable
    {
        private readonly ViewMainMenu _viewMainMenu;
        private readonly MainMenuViewModel _mainMenuViewModel;


        public MainMenuBinder(ViewMainMenu viewMainMenu, MainMenuViewModel mainMenuViewModel)
        {
            _viewMainMenu = viewMainMenu;
            _mainMenuViewModel = mainMenuViewModel;
        }

        public void Initialize()
        {
            _viewMainMenu.ExitGameButton
                .OnClickAsObservable()
                .Subscribe(_mainMenuViewModel.ExitCommand.Execute)
                .AddTo(_viewMainMenu);
     
            _viewMainMenu.StartGameButton
                .OnClickAsObservable()
                .Subscribe((_mainMenuViewModel.GameplayTransitionCommand.Execute))
                .AddTo(_viewMainMenu);
        }
    }
}