
using Cysharp.Threading.Tasks;
using R3;

namespace _Game.Logic.MetaService.AuthenticatorService
{
    public interface IAuthenticatorService
    {
        bool IsSignedIn();
        UniTask WaitSignIn();
        UniTask SignIn();
    }
}
