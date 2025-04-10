using UnityEngine;

namespace _Game.Gameplay.Logic.Service.ObjectPool
{
    public class Containers : MonoBehaviour
    {
        [field: SerializeField]
        public Transform Bullet { get; private set; }

        [field: SerializeField]
        public Transform Laser { get; private set; }

        [field: SerializeField]
        public Transform UFO { get; private set; }
        
        [field: SerializeField]
        public Transform Comet { get; private set; }
    }
}
