using System;
using System.Collections.Generic;
using _Game.Logic.MetaService.Addressable;
using _Game.MainMenu.Logic.Infrastructure.GameObjectContext;
using _Game.MainMenu.Logic.UI;
using _Game.MainMenu.Logic.UI.Authenticator;
using _Game.MainMenu.Logic.UI.Store;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class EntryPointMainMenu : IInitializable, IDisposable
    {
        private readonly IAddressableService _addressableService;
        private readonly FactoryUI _factoryUI;
        private readonly List<GameObject> _addressableResources = new();

        public EntryPointMainMenu(IAddressableService addressableService,
            FactoryUI factoryUI)
        {
            _addressableService = addressableService;
            _factoryUI = factoryUI;
        }

        public async void Initialize()
        {
            try
            {
                await CreateAddressablePrefab<ViewMainMenu>();
                await CreateAddressablePrefab<ViewScore>();
                await CreateAddressablePrefab<StoreInstaller>();
                await CreateAddressablePrefab<AuthenticatorView>();
                await CreateAddressablePrefab<LoaderView>();
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

        private async UniTask CreateAddressablePrefab<T>()
        {
            try
            {
                var prefab = await _addressableService.LoadPrefab<T>();
                _factoryUI.Create(prefab);
                _addressableResources.Add(prefab);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}