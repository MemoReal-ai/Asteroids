using _Game.Gameplay.Logic.Service;

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

        public void OnGoToGameplayScene()
        {
            _sceneHandler.SceneTransition("Gameplay");
        }
        
        public void OnExitGameplayScene()
        {
            _sceneHandler.Quit();
        }
    }
}