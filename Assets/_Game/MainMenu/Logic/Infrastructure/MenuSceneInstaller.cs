using _Game.Gameplay.Logic.Service;
using _Game.MainMenu.Logic.UI;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private ViewMainMenu _viewMainMenu;

        public override void InstallBindings()
        {
            InstallUI();
        }

        private void InstallUI()
        {
            Container.Bind<SceneHandler>().AsCached();
            Container.BindInterfacesAndSelfTo<MainMenuViewModel>().AsCached().NonLazy();
            var viewMainMenu = Container.InstantiatePrefabForComponent<ViewMainMenu>(_viewMainMenu);
            Container.Bind<ViewMainMenu>().FromInstance(viewMainMenu).AsSingle();
        }
    }
}