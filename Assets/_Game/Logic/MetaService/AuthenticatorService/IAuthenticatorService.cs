
using Cysharp.Threading.Tasks;

namespace _Game.Logic.MetaService.AuthenticatorService
{
    public interface IAuthenticatorService
    {
        UniTask SignIn();
        bool IsSignedIn();
        UniTask WaitSignIn();
    }
}
