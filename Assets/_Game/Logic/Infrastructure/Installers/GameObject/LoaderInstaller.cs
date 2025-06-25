using _Game.Logic.UI.MainMenu.Loader;
using _Game.MainMenu.Logic.UI;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class LoaderInstaller : MonoInstaller
    {
        [SerializeField] private LoaderView _loaderView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LoaderChoisenDataViewModel>().AsCached().NonLazy();
            Container.Bind<LoaderView>().FromInstance(_loaderView).AsCached();
        }
    }
}