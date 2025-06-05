using _Game.Gameplay.Logic.UI;
using _Game.Gameplay.Logic.UI.LoseUI;
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
