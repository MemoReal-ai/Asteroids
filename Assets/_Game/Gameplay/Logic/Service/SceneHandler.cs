using System;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

namespace _Game.Gameplay.Logic.Service
{
    public class SceneHandler
    {
        private const string MAINMENUSCENE = "MainMenu";
        private const string GAMEPLAYSCENE = "Gameplay";
        public event Action OnSceneRestart;

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            OnSceneRestart?.Invoke();
        }

        public void SceneTransition(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void TransitionToGameplayScene()
        {
            SceneManager.LoadScene(GAMEPLAYSCENE);
        }

        public void TransitionToMainMenuScene()
        {
            SceneManager.LoadScene(MAINMENUSCENE);
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}