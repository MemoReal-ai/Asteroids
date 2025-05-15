using System;
using _Game.Gameplay.Logic.Service;
using Zenject;
using R3;
using UnityEngine;

namespace _Game.MainMenu.Logic.UI
{
    public class MainMenuViewModel : IInitializable, IDisposable
    {
        private readonly SceneHandler _sceneHandler;
        public ReactiveCommand GameplayTransitionCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand ExitCommand { get; private set; } = new ReactiveCommand();

        public MainMenuViewModel(SceneHandler sceneHandler)
        {
            _sceneHandler = sceneHandler;
        }


        public void Initialize()
        {
            GameplayTransitionCommand.Subscribe(_ => OnGoToGameplayScene());
            ExitCommand.Subscribe(_=>OnExitGameplayScene());
        }

        public void Dispose()
        {
            GameplayTransitionCommand?.Dispose();
            ExitCommand?.Dispose();
        }

        private void OnGoToGameplayScene()
        {
            _sceneHandler.SceneTransition("Gameplay");
        }

        private void OnExitGameplayScene()
        {
            _sceneHandler.Quit();
        }
    }
}