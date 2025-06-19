using _Game.MainMenu.Logic.UI;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class ScoreViewInstaller : MonoInstaller
    {
        [SerializeField] private ViewScore _viewScore;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ViewScoreModelView>().AsCached().NonLazy();
            Container.Bind<ViewScore>().FromInstance(_viewScore).AsCached();
        }
    }
}