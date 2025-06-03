using System;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class TransitorData : IInitializable, IDisposable
    {
        private readonly DataHandler _dataHandler;
        private readonly ShipAbstract _ship;

        public TransitorData(DataHandler dataHandler, ShipAbstract ship)
        {
            _dataHandler = dataHandler;
            _ship = ship;
        }

        public void Initialize()
        {
            _ship.OnLoseLastLife += EndLastLifeSaver;
        }

        public void Dispose()
        {
            _ship.OnLoseLastLife -= EndLastLifeSaver;
        }

        private void EndLastLifeSaver()
        {
            _dataHandler.LocalSaveData();
            _dataHandler.CloudSaveData();
        }
    }
}