using System;
using _Game.Gameplay.Logic.Ship;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class GameTimeHandler : IInitializable, IDisposable
    {
        public event Action OnPaused;
        public event Action OnResume;

        private readonly ShipAbstract _ship;

        public GameTimeHandler(ShipAbstract ship)
        {
            _ship = ship;
        }

        public void Initialize()
        {
            Unpause();
           // _ship.OnShipDestroyed += Pause;
        }

        public void Dispose()
        {
          //  _ship.OnShipDestroyed -= Pause;
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