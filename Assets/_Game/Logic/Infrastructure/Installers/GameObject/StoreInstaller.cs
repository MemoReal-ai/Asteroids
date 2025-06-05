using _Game.MainMenu.Logic.UI.Store;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure.GameObjectContext
{
    public class StoreInstaller : MonoInstaller
    {
        [SerializeField] private StorePopupView _storePopupView;

        public override void InstallBindings()
        {
            Container.Bind<StorePopupView>().FromInstance(_storePopupView);
            Container.BindInterfacesAndSelfTo<StoreBinder>().AsSingle().NonLazy();
        }
    }
}