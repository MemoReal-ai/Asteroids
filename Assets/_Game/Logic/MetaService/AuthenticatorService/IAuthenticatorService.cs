
using Cysharp.Threading.Tasks;

namespace _Game.AuthenticatorService
{
    public interface IAuthenticatorService
    {
        UniTask SignIn();
        bool IsSignedIn();
        UniTask WaitSignIn();
    }
}
