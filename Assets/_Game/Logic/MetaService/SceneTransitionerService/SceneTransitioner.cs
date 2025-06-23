using System;
using R3;
using UnityEngine.Device;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Game.Logic.MetaService.SceneTransitionerService
{
    public class SceneTransitioner : IInitializable, IDisposable
    {
        private const string MAIN_MENU_SCENE = "MainMenu";
        private const string GAMEPLAY_SCENE = "Gameplay";
        public event Action OnSceneDestroy;

        public ReactiveCommand GameplayTransitionCommand { get; } = new();
        public ReactiveCommand ExitGameCommand { get; } = new();
        public ReactiveCommand MainMenuTransitionCommand { get; } = new();
        public ReactiveCommand RestartGameCommand { get; } = new();

        public void Initialize()
        {
            SubscribeProperty();
        }

        public void Dispose()
        {
            DisposeProperty();
        }

        private void RestartGameplay()
        {
            LoadGameplayScene();
            OnSceneDestroy?.Invoke();
        }

        private void LoadGameplayScene()
        {
            SceneManager.LoadScene(GAMEPLAY_SCENE);
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE);
            OnSceneDestroy?.Invoke();
        }

        private void Quit()
        {
            Application.Quit();
        }

        private void DisposeProperty()
        {
            GameplayTransitionCommand?.Dispose();
            ExitGameCommand?.Dispose();
            MainMenuTransitionCommand?.Dispose();
        }

        private void SubscribeProperty()
        {
            GameplayTransitionCommand.Subscribe(x => LoadGameplayScene());
            MainMenuTransitionCommand.Subscribe(x => LoadMainMenu());
            ExitGameCommand.Subscribe(x => Quit());
            RestartGameCommand.Subscribe(x => RestartGameplay());
        }
    }
}