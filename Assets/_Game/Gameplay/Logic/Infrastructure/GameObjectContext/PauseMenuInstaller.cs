using _Game.Gameplay.Logic.UI;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure.GameObjectContext
{
    public class PauseMenuInstaller : MonoInstaller
    {
        [SerializeField] private PauseView _pauseView;

        public override void InstallBindings()
        {
            Container.Bind<PauseView>().FromInstance(_pauseView).AsSingle();
            Container.BindInterfacesTo<PauseViewBinder>().AsSingle().NonLazy();
        }
    }
}