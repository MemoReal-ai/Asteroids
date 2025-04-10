using System;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using Zenject;

namespace _Game.Gameplay.Logic.UI
{
    public class PresenterViewLose : IInitializable, IDisposable
    {
        private readonly ShipAbstract _ship;
        private readonly LoseView _loseView;
        private readonly SceneHandler _sceneHandler;

        public PresenterViewLose(ShipAbstract ship, LoseView loseView, SceneHandler sceneHandler)
        {
            _loseView = loseView;
            _ship = ship;
            _sceneHandler = sceneHandler;
        }


        public void Initialize()
        {
            _loseView.gameObject.SetActive(false);
            _ship.OnShipDestroyed += Show;
            _loseView.RestartButton.onClick.AddListener(_sceneHandler.Restart);
            _loseView.QuitButton.onClick.AddListener(() => _sceneHandler.SceneTransition("MainMenu"));
        }

        private void Show()
        {
            _loseView.gameObject.SetActive(true);
        }

        public void Dispose()
        {
            _ship.OnShipDestroyed -= Show;
            _loseView.RestartButton.onClick.RemoveListener(_sceneHandler.Restart);
            _loseView.QuitButton.onClick.RemoveListener(()=>_sceneHandler.SceneTransition("MainMenu"));
            
        }
    }
}