using System;
using _Game.Logic.MetaService.Addressable;
using _Game.MainMenu.Logic.Infrastructure.GameObjectContext;
using _Game.MainMenu.Logic.UI;
using _Game.MainMenu.Logic.UI.Authenticator;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.EntryPoints
{
    public class EntryPointMainMenu : IInitializable
    {
        private readonly IAddressableService _addressableService;
        private readonly FactoryUI _factoryUI;

        public EntryPointMainMenu(IAddressableService addressableService,
            FactoryUI factoryUI)
        {
            _addressableService = addressableService;
            _factoryUI = factoryUI;
        }

        public void Initialize()
        {
            try
            {
                UniTask.WhenAll(_addressableService.LoadPrefab<ViewMainMenu>(_factoryUI),
                    _addressableService.LoadPrefab<ViewScore>(_factoryUI),
                    _addressableService.LoadPrefab<StoreInstaller>(_factoryUI),
                    _addressableService.LoadPrefab<AuthenticatorView>(_factoryUI),
                    _addressableService.LoadPrefab<LoaderView>(_factoryUI));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}