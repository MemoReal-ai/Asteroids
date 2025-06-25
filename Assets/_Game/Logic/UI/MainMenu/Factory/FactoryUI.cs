using UnityEngine;
using Zenject;

namespace _Game.Logic.UI.MainMenu.Factory
{
    public class FactoryUI
    {
        private readonly IInstantiator _instantiator;

        public FactoryUI(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public T Create<T>(Object prefab)
        {
            T window = _instantiator.InstantiatePrefabForComponent<T>(prefab);
            return window;
        }
    }
}