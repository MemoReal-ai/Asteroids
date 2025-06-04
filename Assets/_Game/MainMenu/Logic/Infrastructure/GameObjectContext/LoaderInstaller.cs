using _Game.MainMenu.Logic.UI;
using _Game.MainMenu.Logic.UI.Loader;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure.GameObjectContext
{
    public class LoaderInstaller : MonoInstaller
    {
        [SerializeField] private LoaderView _loaderView;

        public override void InstallBindings()
        {
            Container.Bind<LoaderView>().FromInstance(_loaderView).AsCached();
            Container.BindInterfacesAndSelfTo<LoaderBinder>().AsSingle().NonLazy();
        }
    }
}