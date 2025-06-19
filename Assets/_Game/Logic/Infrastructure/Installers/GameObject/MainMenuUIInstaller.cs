using _Game.Logic.UI.MainMenu.MainMenu;
using _Game.MainMenu.Logic.UI;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class MainMenuUIInstaller : MonoInstaller
    {
        [SerializeField] private ViewMainMenu _viewMainMenu;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuViewModel>().AsCached().NonLazy();
            Container.Bind<ViewMainMenu>().FromInstance(_viewMainMenu).AsCached();
        }
    }
}