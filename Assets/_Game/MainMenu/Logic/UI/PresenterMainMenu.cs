using System;
using _Game.Gameplay.Logic.Service;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.UI
{
    public class PresenterMainMenu : IInitializable, IDisposable
    {
        private readonly ViewMainMenu _viewMainMenu;
        private readonly SceneHandler _sceneHandler;

        public PresenterMainMenu(ViewMainMenu viewMainMenu, SceneHandler sceneHandler)
        {
            _viewMainMenu = viewMainMenu;
            _sceneHandler = sceneHandler;
        }

        public void Initialize()
        {
            _viewMainMenu.StartGameButton.onClick.AddListener(() => { _sceneHandler.SceneTransition("Gameplay"); });
            _viewMainMenu.ExitGameButton.onClick.AddListener(_sceneHandler.Quit);
        }


        public void Dispose()
        {
            _viewMainMenu.StartGameButton.onClick.RemoveListener(() => _sceneHandler.SceneTransition("Gameplay"));
            _viewMainMenu.ExitGameButton.onClick.RemoveListener(_sceneHandler.Quit);
        }
    }
}