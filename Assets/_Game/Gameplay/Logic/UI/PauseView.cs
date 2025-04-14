using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class PauseView : MonoBehaviour
    {
        [field: SerializeField] public Button ResumeButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }


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
