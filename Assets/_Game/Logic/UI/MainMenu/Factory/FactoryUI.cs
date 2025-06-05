using System;
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

        public GameObject Create<T>(GameObject userInterface)
        {
            GameObject window = _instantiator.InstantiatePrefab(userInterface);

            if (window.TryGetComponent(out T _))
            {
                return window;
            }

            throw new Exception($"Not  found component {typeof(T)}");
        }
    }
}