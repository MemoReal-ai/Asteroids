using _Game.Gameplay.Logic.UI;
using _Game.Gameplay.Logic.UI.LoseVVM;
using _Game.MainMenu.Logic.UI;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure.GameObjectContext
{
    public class LoseViewInstaller : MonoInstaller
    {
        [SerializeField] private LoseView _loseView;
        public override void InstallBindings()
        {
            Container.Bind<LoseView>().FromInstance(_loseView).AsSingle();
            Container.BindInterfacesTo<BinderLoseView>().AsSingle().NonLazy();
        }
   
    }
}
