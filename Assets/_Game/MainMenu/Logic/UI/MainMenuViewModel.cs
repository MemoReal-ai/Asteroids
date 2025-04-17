using _Game.Gameplay.Logic.Service;
using MVVM;

namespace _Game.MainMenu.Logic.UI
{
    public class MainMenuViewModel
    {
        private readonly ViewMainMenu _viewMainMenu;
        private readonly SceneHandler _sceneHandler;

        public MainMenuViewModel(SceneHandler sceneHandler)
        {
            _sceneHandler = sceneHandler;
        }

        [Method("OnGoToGameplayScene")]
        public void OnGoToGameplayScene()
        {
            _sceneHandler.SceneTransition("Gameplay");
        }

        [Method("OnExitGameplayScene")]
        public void OnExitGameplayScene()
        {
            _sceneHandler.Quit();
        }
    }
}