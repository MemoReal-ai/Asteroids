using _Game.MainMenu.Logic.UI;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure.GameObjectContext
{
    public class ScoreViewInstaller : MonoInstaller
    {
        [SerializeField] private ViewScore _viewScore;

        public override void InstallBindings()
        {
            Container.Bind<ViewScore>().FromInstance(_viewScore).AsSingle();
            Container.BindInterfacesTo<BinderViewScore>().AsSingle().NonLazy();
        }
    }
}