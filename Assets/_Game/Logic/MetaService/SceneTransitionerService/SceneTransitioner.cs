using System;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

namespace _Game.Logic.MetaService.SceneTransitionerService
{
    public class SceneTransitioner
    {
        private const string MAIN_MENU_SCENE = "MainMenu";
        private const string GAMEPLAY_SCENE = "Gameplay";
        public event Action OnDestroyGameplayScene;

        public void RestartGameplay()
        {
            LoadGameplayScene();
            OnDestroyGameplayScene?.Invoke();
        }

        public void LoadGameplayScene()
        {
            SceneManager.LoadScene(GAMEPLAY_SCENE);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE);
            OnDestroyGameplayScene?.Invoke();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}