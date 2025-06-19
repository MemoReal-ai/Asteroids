using _Game.Logic.UI.MainMenu.Store;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class StoreInstaller : MonoInstaller
    {
        [SerializeField] private StorePopupView _storePopupView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StoreViewModel>().AsCached().NonLazy();
            Container.Bind<StorePopupView>().FromInstance(_storePopupView);
        }
    }
}