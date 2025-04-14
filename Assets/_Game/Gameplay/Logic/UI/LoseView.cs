using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class LoseView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pointLabel;
        [field: SerializeField]
        public Button RestartButton { get; private set; }

        [field: SerializeField]
        public Button QuitButton { get; private set; }

        public void SetPoint(string points)
        {
            _pointLabel.text = $"You points - {points}";
        }
    }
}