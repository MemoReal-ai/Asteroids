using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class PauseView : MonoBehaviour
    { 
        public Button ResumeButton;
       
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