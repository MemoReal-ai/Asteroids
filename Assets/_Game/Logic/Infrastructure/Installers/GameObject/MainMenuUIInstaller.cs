using _Game.MainMenu.Logic.UI;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure.GameObjectContext
{
    public class MainMenuUIInstaller : MonoInstaller
    {
        [SerializeField] private ViewMainMenu _viewMainMenu;

        public override void InstallBindings()
        {
            Container.Bind<ViewMainMenu>().FromInstance(_viewMainMenu).AsCached();
            Container.BindInterfacesTo<MainMenuBinder>().AsSingle().NonLazy();
        }
    }
}