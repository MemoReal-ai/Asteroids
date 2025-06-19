using _Game.Gameplay.Logic.UI;
using _Game.Logic.UI.Gameplay.Pause;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class PauseMenuInstaller : MonoInstaller
    {
        [SerializeField] private PauseView _pauseView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PauseViewModel>().AsCached().NonLazy();
            Container.Bind<PauseView>().FromInstance(_pauseView).AsSingle();
        }
    }
}