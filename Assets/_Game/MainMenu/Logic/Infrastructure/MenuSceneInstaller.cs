using _Game.MainMenu.Logic.UI;
using _Game.MainMenu.Logic.UI.Store;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class MenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallEntryPoint();
            InstallFactories();
            InstallUI();
        }

        private void InstallFactories()
        {
            Container.Bind<FactoryUI>().AsCached().NonLazy();
        }

        private void InstallEntryPoint()
        {
            Container.BindInterfacesAndSelfTo<EntryPointMainMenu>().AsSingle().NonLazy();
        }

        private void InstallUI()
        {
            Container.BindInterfacesAndSelfTo<ViewScoreModelView>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<MainMenuViewModel>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<StoreViewModel>().AsCached().NonLazy();
        }
    }
}