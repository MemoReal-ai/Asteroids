using _Game.MainMenu.Logic.UI;
using _Game.MainMenu.Logic.UI.Loader;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class LoaderInstaller : MonoInstaller
    {
        [SerializeField] private LoaderView _loaderView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LoaderViewModel>().AsCached().NonLazy();
            Container.Bind<LoaderView>().FromInstance(_loaderView).AsCached();
        }
    }
}