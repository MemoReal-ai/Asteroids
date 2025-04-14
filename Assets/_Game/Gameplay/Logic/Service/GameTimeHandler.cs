using System;
using _Game.Gameplay.Logic.Ship;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class GameTimeHandler : IInitializable, IDisposable
    {
        private readonly ShipAbstract _ship;

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
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            Time.timeScale = 1;
        }
    }
}