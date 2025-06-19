using _Game.Gameplay.Logic.UI.AdsView;
using _Game.Logic.UI.Gameplay.AdsView;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class AdsPopupInstaller : MonoInstaller
    {
        [SerializeField] private AdsPopupView _adsPopupView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AdsViewModel>().AsCached();
            Container.Bind<AdsPopupView>().FromInstance(_adsPopupView).AsSingle();
        }
    }
}