using UnityEngine;
using UnityEngine.UI;

namespace _Game.MainMenu.Logic.UI
{
    public class ViewMainMenu : MonoBehaviour
    {
        [field: SerializeField] public Button StartGameButton { get; private set; }
        [field: SerializeField] public Button ExitGameButton { get; private set; }
    }
}