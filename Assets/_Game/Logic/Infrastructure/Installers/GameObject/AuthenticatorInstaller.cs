using _Game.MainMenu.Logic.UI.Authenticator;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure.GameObjectContext
{
    public class AuthenticatorInstaller : MonoInstaller
    {
        [SerializeField] private AuthenticatorView _authenticatorView;

        public override void InstallBindings()
        {
            Container.Bind<AuthenticatorView>().FromInstance(_authenticatorView).AsSingle();
            Container.BindInterfacesAndSelfTo<AuthenticatorBinder>().AsCached().NonLazy();
        }
    }
}