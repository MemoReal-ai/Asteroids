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