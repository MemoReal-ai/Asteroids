using UnityEngine;
using UnityEngine.UI;

namespace _Game.MainMenu.Logic.UI.Store
{
    public class StorePopupView : MonoBehaviour
    {
        public Button CloseButton;
        public Button PaymentButton;
        public Button ShowPopUpButton;

        private void Start()
        {
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}