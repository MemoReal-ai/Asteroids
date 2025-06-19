using _Game.Logic.UI.MainMenu.Authenticator;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Infrastructure.Installers.GameObject
{
    public class AuthenticatorInstaller : MonoInstaller
    {
        [SerializeField] private AuthenticatorView _authenticatorView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AuthenticatorViewModel>().AsCached().NonLazy();
            Container.Bind<AuthenticatorView>().FromInstance(_authenticatorView).AsSingle();
        }
    }
}