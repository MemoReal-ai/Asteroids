using System;
using Cysharp.Threading.Tasks;
using R3;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Zenject;

namespace _Game.Logic.MetaService.AuthenticatorService
{
    public class AuthenticatorService : IAuthenticatorService, IInitializable, IDisposable
    {
        private readonly UniTaskCompletionSource _completionSource = new();

        public ReactiveCommand SignInCommand { get; } = new();

        public async void Initialize()
        {
            try
            {
                await UnityServices.InitializeAsync();
                SubscribeProperty();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            DisposeProperty();
        }

        private void DisposeProperty()
        {
            SignInCommand?.Dispose();
        }

        private void SubscribeProperty()
        {
            SignInCommand.Subscribe(x => { _ = SignIn(); });
        }

        private async UniTask SignIn()
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            _completionSource.TrySetResult();
        }

        public bool IsSignedIn()
        {
            return AuthenticationService.Instance.IsSignedIn;
        }

        public async UniTask WaitSignIn()
        {
            await _completionSource.Task;
        }
    }
}