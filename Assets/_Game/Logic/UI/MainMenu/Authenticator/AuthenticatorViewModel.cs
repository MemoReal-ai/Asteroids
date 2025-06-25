using System;
using _Game.Logic.MetaService.AuthenticatorService;
using R3;
using Zenject;

namespace _Game.Logic.UI.MainMenu.Authenticator
{
    public class AuthenticatorViewModel : IInitializable, IDisposable
    {
        private readonly AuthenticatorView _authenticatorView;
        private readonly IAuthenticatorService _authenticatorService;

        private ReactiveCommand SignInCommand { get; } = new();

        public AuthenticatorViewModel(IAuthenticatorService authenticatorService, AuthenticatorView authenticatorView)
        {
            _authenticatorService = authenticatorService;
            _authenticatorView = authenticatorView;
        }

        public void Initialize()
        {
            if (_authenticatorService.IsSignedIn())
            {
                return;
            }

            _authenticatorView.Show();
            SubscribeProperty();
            Bind();
        }

        public void Dispose()
        {
            SignInCommand?.Dispose();
        }

        private void SubscribeProperty()
        {
            SignInCommand.Subscribe(_ => _authenticatorService.SignIn());
        }

        private void Bind()
        {
            _authenticatorView.LoginButton.OnClickAsObservable().Subscribe(x =>
                {
                    SignInCommand.Execute(x);
                    _authenticatorView.Hide();
                })
                .AddTo(_authenticatorView);
        }
    }
}