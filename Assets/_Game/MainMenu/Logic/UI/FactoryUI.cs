using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.UI
{
    public class FactoryUI
    {
        private readonly IInstantiator _instantiator;
        
        public FactoryUI(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public GameObject Create(GameObject userInterface)
        {
            GameObject window = _instantiator.InstantiatePrefab(userInterface);
            return window;
        }
    }
}