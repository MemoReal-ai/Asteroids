using System;
using _Game.Gameplay.Logic.Service;


namespace _Game.Gameplay.Logic.UI
{
    public class PauseViewModel
    {
        public event Action OnRemovePause;

        private readonly SceneHandler _sceneHandler;
        private readonly GameTimeHandler _gameTimeHandler;

        public PauseViewModel(SceneHandler sceneHandler, GameTimeHandler gameTimeHandler)
        {
            _sceneHandler = sceneHandler;
            _gameTimeHandler = gameTimeHandler;
        }
        
        public void ResumeGame()
        {
            _gameTimeHandler.Unpause();
            OnRemovePause?.Invoke();
        }
        
        public void GoToMainMenu()
        {
            _sceneHandler.SceneTransition("MainMenu");
        }
        
    }
}