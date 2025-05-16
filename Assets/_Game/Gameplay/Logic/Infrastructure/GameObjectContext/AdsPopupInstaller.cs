using _Game.Gameplay.Logic.UI.AdsView;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure.GameObjectContext
{
    public class AdsPopupInstaller : MonoInstaller
    {
        [SerializeField] private AdsPopupView _adsPopupView;
        public override void InstallBindings()
        {
            Container.Bind<AdsPopupView>().FromInstance(_adsPopupView).AsSingle();
            Container.BindInterfacesAndSelfTo<AdsBinder>().AsSingle().NonLazy();
        }
    }
}