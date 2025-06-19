using _Game.Gameplay.Logic.UI;
using _Game.Logic.UI.Gameplay.LoseView;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class LoseViewInstaller : MonoInstaller
    {
        [SerializeField] private LoseView _loseView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ViewModelLose>().AsCached().NonLazy();
            Container.Bind<LoseView>().FromInstance(_loseView).AsSingle();
        }
    }
}