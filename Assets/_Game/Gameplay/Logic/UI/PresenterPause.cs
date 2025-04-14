using System;
using _Game.Gameplay.Logic.Service;
using Zenject;

namespace _Game.Gameplay.Logic.UI
{
    public class PresenterPause : IInitializable, IDisposable
    {
        private readonly PauseView _pauseView;
        private readonly SceneHandler _sceneHandler;
        private readonly GameTimeHandler _gameTimeHandler;

        public PresenterPause(PauseView pauseView, SceneHandler sceneHandler, GameTimeHandler gameTimeHandler)
        {
            _pauseView = pauseView;
            _sceneHandler = sceneHandler;
            _gameTimeHandler = gameTimeHandler;
        }

        public void Initialize()
        {
            _pauseView.ExitButton.onClick.AddListener(() => _sceneHandler.SceneTransition("MainMenu"));
            _pauseView.ResumeButton.onClick.AddListener(() =>
            {
                _pauseView.Hide();
                _gameTimeHandler.Unpause();
            });
            _pauseView.Hide();
        }

        public void Dispose()
        {
            _pauseView.ExitButton.onClick.RemoveListener(() => _sceneHandler.SceneTransition("MainMenu"));
            _pauseView.ResumeButton.onClick.RemoveListener(_pauseView.Hide);
        }

        public void Show()
        {
            _pauseView.Show();
        }
    }
}