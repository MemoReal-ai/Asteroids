using UnityEngine;
using Zenject;

namespace _Game
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
            GameObject window = _instantiator.InstantiatePrefab(userInterface, Vector3.zero, Quaternion.identity, null);
            return window;
        }
    }
}