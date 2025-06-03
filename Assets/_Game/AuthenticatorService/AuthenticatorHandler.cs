using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

namespace _Game.AuthenticatorService
{
    public class AuthenticatorHandler : IAuthenticatorService, IInitializable
    {
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
            Debug.Log($"AuthenticatorService initialized {AuthenticationService.Instance.PlayerId}");
        }

        public bool IsSignedIn()
        {
            return AuthenticationService.Instance.IsSignedIn;
        }
    }
}