using _Game.Gameplay.Logic.UI;
using _Game.Logic.UI.Gameplay.UserStatsView;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class UIStatsInstaller : MonoInstaller
    {
        [SerializeField] private UserView _userView;
        [SerializeField] private ReloadView _reloadView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ViewModelUserStats>().AsSingle().NonLazy();
            Container.Bind<UserView>().FromInstance(_userView).AsSingle();
            Container.Bind<ReloadView>().FromInstance(_reloadView).AsSingle();
            Container.BindInterfacesAndSelfTo<PresenterReloadView>().AsSingle().NonLazy();
        }
    }
}