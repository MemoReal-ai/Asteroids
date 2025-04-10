using System;
using _Game.Gameplay.Logic.Ship;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class LoseHandler : IInitializable, IDisposable
    {
        private readonly ShipAbstract _ship;

        public LoseHandler(ShipAbstract ship)
        {
            _ship = ship;
        }

        public void Initialize()
        {
            Time.timeScale = 1;
            _ship.OnShipDestroyed += Lose;
        }

        private void Lose()
        {
            Time.timeScale = 0;
        }


        public void Dispose()
        {
            _ship.OnShipDestroyed -= Lose;
        }
    }
}