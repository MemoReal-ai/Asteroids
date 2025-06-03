using System;
using _Game.AuthenticatorService;
using R3;
using Zenject;

namespace _Game.MainMenu.Logic.UI.Authenticator
{
    public class AuthenticatorViewModel : IInitializable, IDisposable
    {
        public ReactiveCommand SignInCommand { get; private set; } = new ReactiveCommand();

        private readonly IAuthenticatorService _authenticatorService;

        public AuthenticatorViewModel(IAuthenticatorService authenticatorService)
        {
            _authenticatorService = authenticatorService;
        }

        public void Initialize()
        {
            SignInCommand.Subscribe(x => _authenticatorService.SignIn());
        }

        public void Dispose()
        {
            SignInCommand?.Dispose();
        }
    }
}