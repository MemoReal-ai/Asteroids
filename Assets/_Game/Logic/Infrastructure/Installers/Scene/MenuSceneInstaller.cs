using _Game.Logic.Infrastructure.EntryPoints;
using _Game.Logic.UI.MainMenu.Factory;
using _Game.MainMenu.Logic.UI;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.Scene
{
    public class MenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallEntryPoint();
            InstallFactories();
        }

        private void InstallFactories()
        {
            Container.Bind<FactoryUI>().AsCached().NonLazy();
        }

        private void InstallEntryPoint()
        {
            Container.BindInterfacesAndSelfTo<EntryPointMainMenu>().AsSingle().NonLazy();
        }
        
    }
}