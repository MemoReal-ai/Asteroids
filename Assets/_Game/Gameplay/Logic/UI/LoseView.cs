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
            gameObject.SetActive(false);
        }
    }
}