using System;
using _Game.Gameplay.Logic.Ship;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class GameTimeHandler : IInitializable, IDisposable
    {
        private readonly ShipAbstract _ship;

        public event Action OnPaused;
        public event Action OnResume;

        public GameTimeHandler(ShipAbstract ship)
        {
            _ship = ship;
        }

        public void Initialize()
        {
            Unpause();
            _ship.OnShipDestroyed += Pause;
        }

        public void Dispose()
        {
            _ship.OnShipDestroyed -= Pause;
        }

        public void Pause()
        {
            OnPaused?.Invoke();
        }

        public void Unpause()
        {
            OnResume?.Invoke();
        }
    }
}