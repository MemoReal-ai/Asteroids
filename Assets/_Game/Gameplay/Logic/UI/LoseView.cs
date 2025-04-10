using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class LoseView : MonoBehaviour
    {
       
        [field: SerializeField]
        public Button RestartButton { get; private set; }
        
        [field: SerializeField]
        public Button QuitButton { get; private set; }
        
    }
}