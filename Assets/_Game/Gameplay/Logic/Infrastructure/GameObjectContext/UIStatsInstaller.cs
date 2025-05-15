using _Game.Gameplay.Logic.UI;
using _Game.Gameplay.Logic.UI.UserStatsVVM;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure.GameObjectContext
{
    public class UIStatsInstaller : MonoInstaller
    {
        [SerializeField] private UserView _userView;
        [SerializeField] private ReloadView _reloadView;

        public override void InstallBindings()
        {
            Container.Bind<UserView>().FromInstance(_userView).AsSingle();
            Container.BindInterfacesTo<BinderUserStats>().AsSingle().NonLazy();
            Container.Bind<ReloadView>().FromInstance(_reloadView).AsSingle();
            Container.BindInterfacesAndSelfTo<PresenterReloadView>().AsSingle().NonLazy();
        }
    }
}