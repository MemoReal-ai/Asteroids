using UnityEngine;
using UnityEngine.UI;

namespace _Game.MainMenu.Logic.UI
{
    public class StorePopupView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _paymentButton;

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