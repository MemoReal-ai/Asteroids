using System;
using Cysharp.Threading.Tasks;
using R3;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Zenject;

namespace _Game.Logic.MetaService.AuthenticatorService
{
    public class AuthenticatorService : IAuthenticatorService, IInitializable
    {
        private readonly UniTaskCompletionSource _completionSource = new();
        public async void Initialize()
        {
            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public async UniTask SignIn()
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