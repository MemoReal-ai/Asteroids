using System;
using _Game.Gameplay.Logic.Service;
using Zenject;

namespace _Game.MainMenu.Logic.UI.Loader
{
    public class LoaderViewModel : IInitializable, IDisposable
    {
        private readonly DataHandler _dataHandler;

        public LoaderViewModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}