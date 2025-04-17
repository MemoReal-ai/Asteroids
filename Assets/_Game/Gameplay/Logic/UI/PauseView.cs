using System;
using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class PauseView : MonoBehaviour
    { 
        [Data("ResumeGame")]
        public Button ResumeButton;
       
        [Data("GoToMainMenu")]
        public Button ExitButton;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}