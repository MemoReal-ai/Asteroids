using System;
using _Game.AuthenticatorService;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

namespace _Game.Logic.MetaService.AuthenticatorService
{
    public class AuthenticatorHandler : IAuthenticatorService, IInitializable
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
                throw;
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