using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.MainMenu.Logic.UI.Authenticator
{
    public class AuthenticatorView : MonoBehaviour
    {
        public Button LoginButton;

        [SerializeField] private CanvasGroup _canvasGroup;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
        }
    }
}