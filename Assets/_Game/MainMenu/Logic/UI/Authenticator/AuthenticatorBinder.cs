using _Game.AuthenticatorService;
using R3;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.UI.Authenticator
{
    public class AuthenticatorBinder : IInitializable
    {
        private readonly AuthenticatorViewModel _authenticatorViewModel;
        private readonly AuthenticatorView _authenticatorView;
        private readonly IAuthenticatorService _authenticator;
        private bool _isInitialized = false;

        public AuthenticatorBinder(AuthenticatorViewModel authenticatorViewModel, AuthenticatorView authenticatorView,
            IAuthenticatorService authenticator)
        {
            _authenticatorViewModel = authenticatorViewModel;
            _authenticatorView = authenticatorView;
            _authenticator = authenticator;
        }

        public void Initialize()
        {
            _isInitialized = _authenticator.IsSignedIn();

            if (_isInitialized)
            {
                return;
            }

            _authenticatorView.Show();
            _authenticatorView.LoginButton.OnClickAsObservable().Subscribe(u =>
            {
                _authenticatorViewModel.SignInCommand.Execute(u);
                Hide();
            }).AddTo(_authenticatorView);
        }


        private void Hide()
        {
            _authenticatorView.Hide();
        }
    }
}