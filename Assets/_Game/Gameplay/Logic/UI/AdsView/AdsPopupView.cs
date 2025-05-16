using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI.AdsView
{
    public class AdsPopupView : MonoBehaviour
    {
        public Button AdsButton;
        public Button ExitButton;

        private void Start()
        {
            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}