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

        public void RestartGameplay()
        {
            LoadGameplayScene();
            OnSceneRestart?.Invoke();
        }

        public void LoadGameplayScene()
        {
            SceneManager.LoadScene(GAMEPLAYSCENE);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MAINMENUSCENE);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}