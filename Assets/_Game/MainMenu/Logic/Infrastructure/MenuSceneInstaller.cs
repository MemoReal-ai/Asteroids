using _Game.Gameplay.Logic.Service;
using _Game.MainMenu.Logic.UI;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private ViewMainMenu _viewMainMenu;
        [SerializeField] private ViewScore _viewScore;

        public override void InstallBindings()
        {
            InstallUI();
        }

        private void InstallUI()
        {
            Container.BindInterfacesTo<MainMenuBinder>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuViewModel>().AsCached().NonLazy();
            Container.Bind<ViewMainMenu>().FromComponentInNewPrefab(_viewMainMenu).AsSingle().NonLazy();
            Container.Bind<ViewScore>().FromComponentInNewPrefab(_viewScore).AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<ViewScoreModelView>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<BinderViewScore>().AsCached().NonLazy();
        }
    }
}