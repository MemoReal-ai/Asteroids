using System.Collections.Generic;
using _Game.MainMenu.Logic.UI;
using _Game.MainMenu.Logic.UI.Store;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private List<AssetReference> _prefabs;

        public override void InstallBindings()
        {
            InstallEntryPoint();
            InstallAssetReference();
            InstallFactories();
            InstallUI();
        }

        private void InstallFactories()
        {
            Container.Bind<FactoryUI>().AsCached().NonLazy();
        }

        private void InstallAssetReference()
        {
            Container.Bind<List<AssetReference>>().FromInstance(_prefabs).AsSingle().NonLazy();
        }

        private void InstallEntryPoint()
        {
            Container.BindInterfacesAndSelfTo<EntryPointMainMenu>().AsSingle().NonLazy();
        }

        private void InstallUI()
        {
            Container.BindInterfacesAndSelfTo<ViewScoreModelView>().AsCached();
            Container.BindInterfacesAndSelfTo<MainMenuViewModel>().AsCached();
            Container.BindInterfacesAndSelfTo<StoreViewModel>().AsCached();
        }
    }
}