using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class LoseView : MonoBehaviour
    {
         public TextMeshProUGUI PointLabel;
         public Button RestartButton;
         public Button QuitButton;

        private void Start()
        {
            Hide();
        }

        public void ShowPoints(string points)
        {
            PointLabel.text=points;
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