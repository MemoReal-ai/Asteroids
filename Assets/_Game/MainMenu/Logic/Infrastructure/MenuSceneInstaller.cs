using System.Collections.Generic;
using _Game.MainMenu.Logic.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private ViewMainMenu _viewMainMenu;
        [SerializeField] private ViewScore _viewScore;
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
            Container.BindInterfacesTo<MainMenuBinder>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MainMenuViewModel>().AsCached().NonLazy();
            Container.Bind<ViewMainMenu>().FromInstance(_viewMainMenu).AsSingle().NonLazy();
            Container.Bind<ViewScore>().FromInstance(_viewScore).AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<ViewScoreModelView>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<BinderViewScore>().AsCached().NonLazy();
        }
    }
}