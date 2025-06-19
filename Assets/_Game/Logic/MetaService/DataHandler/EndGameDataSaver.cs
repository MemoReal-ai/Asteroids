using System;
using _Game.Gameplay.Logic.Ship;
using _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler;
using Zenject;

namespace _Game.Logic.MetaService.DataHandler
{
    public class EndGameDataSaver : IInitializable, IDisposable
    {
        private readonly DataSyncManager _dataSyncManager;
        private readonly ShipAbstract _ship;

        public EndGameDataSaver(DataSyncManager dataSyncManager, ShipAbstract ship)
        {
            _dataSyncManager = dataSyncManager;
            _ship = ship;
        }

        public void Initialize()
        {
            _ship.OnLoseLastLife += OnLastLifeSaver;
        }

        public void Dispose()
        {
            _ship.OnLoseLastLife -= OnLastLifeSaver;
        }

        private void OnLastLifeSaver()
        {
            _dataSyncManager.Save();
        }
    }
}