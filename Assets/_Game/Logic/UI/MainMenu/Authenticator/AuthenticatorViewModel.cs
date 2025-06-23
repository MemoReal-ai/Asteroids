using System;
using _Game.Logic.MetaService.AuthenticatorService;
using R3;
using Zenject;

namespace _Game.Logic.UI.MainMenu.Authenticator
{
    public class AuthenticatorViewModel : IInitializable
    {
        private readonly AuthenticatorView _authenticatorView;
        private readonly IAuthenticatorService _authenticatorService;
        
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
            Bind();
        }
        
        private void Bind()
        {
            _authenticatorView.LoginButton.OnClickAsObservable().Subscribe(x =>
                {
                   _authenticatorService.SignInCommand.Execute(x);
                    _authenticatorView.Hide();
                })
                .AddTo(_authenticatorView);
        }
    }
}