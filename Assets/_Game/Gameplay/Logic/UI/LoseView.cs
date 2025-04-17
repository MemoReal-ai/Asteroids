using System;
using MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class LoseView : MonoBehaviour
    {
        [Data("Points")] public TextMeshProUGUI PointLabel;
        [Data("Restart")] public Button RestartButton;
        [Data("Quit")] public Button QuitButton;

        private void Start()
        {
            gameObject.SetActive(false);
        }
    }
}