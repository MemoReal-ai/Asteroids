using System;
using System.Collections.Generic;
using _Game.Addressable;
using _Game.MainMenu.Logic.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class EntryPointMainMenu : IInitializable, IDisposable
    {
        private readonly List<AssetReference> _prefabs;

        private readonly IAddressableService _addressableService;
        private readonly FactoryUI _factoryUI;
        private readonly List<GameObject> _addressableResources = new();

        public EntryPointMainMenu(IAddressableService addressableService,
            FactoryUI factoryUI, List<AssetReference> prefabs)
        {
            _addressableService = addressableService;
            _factoryUI = factoryUI;
            _prefabs = prefabs;
        }

        public async void Initialize()
        {
            try
            {
                foreach (var prefab in _prefabs)
                {
                    var resources = await _addressableService.LoadPrefab(prefab);
                    var objectInstantiate = _factoryUI.Create(resources);
                    _addressableResources.Add(objectInstantiate);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void Dispose()
        {
            try
            {
                foreach (var resource in _addressableResources)
                {
                    _addressableService.UnloadPrefab(resource.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}