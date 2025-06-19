using System;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

namespace _Game.Logic.MetaService.SceneTransitionerService
{
    public class SceneTransitioner
    {
        private const string MAIN_MENU_SCENE = "MainMenu";
        private const string GAMEPLAY_SCENE = "Gameplay";
        public event Action OnSceneDestroy;

        public void RestartGameplay()
        {
            LoadGameplayScene();
            OnSceneDestroy?.Invoke();
        }

        public void LoadGameplayScene()
        {
            SceneManager.LoadScene(GAMEPLAY_SCENE);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE);
            OnSceneDestroy?.Invoke();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}