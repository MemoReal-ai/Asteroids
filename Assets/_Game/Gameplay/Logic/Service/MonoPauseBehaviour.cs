using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public abstract class MonoPauseBehaviour : MonoBehaviour
    {
        private GameTimeHandler _gameTimeHandler;

        [Inject]
        public void Construct(GameTimeHandler gameTimeHandler)
        {
             _gameTimeHandler = gameTimeHandler;
            print(1);
        }

        protected virtual void Start()
        {
            _gameTimeHandler.OnPaused += OnPause;
            _gameTimeHandler.OnResume += OnResume;
        }

        protected virtual void OnDestroy()
        {
            _gameTimeHandler.OnPaused -= OnPause;
            _gameTimeHandler.OnResume -= OnResume;
        }

        protected abstract void OnPause();
        protected abstract void OnResume();
    }
}