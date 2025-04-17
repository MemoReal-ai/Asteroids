using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.MainMenu.Logic.UI
{
    public class ViewMainMenu : MonoBehaviour
    {
        [Data("OnGoToGameplayScene")]
        public Button StartGameButton;
        [Data("OnExitGameplayScene")]
        public Button ExitGameButton;
    }
}