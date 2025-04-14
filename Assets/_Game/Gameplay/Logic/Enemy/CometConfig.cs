using _Game.Gameplay.Logic.Weapon;
using UnityEngine;

namespace _Game.Gameplay.Logic.Enemy
{
    [CreateAssetMenu(fileName = "CometConfig", menuName = "Config/Enemy/Comet/Create", order = 0)]
    public class CometConfig : ScriptableObject
    {
        [field: SerializeField]
        public float DistanceToFade { get; private set; }
        [field: SerializeField]
        public SmallComet SmallComet { get; private set; }
        [field: SerializeField]
        public float MinSpeed { get; private set; }
        [field: SerializeField]
        public int CountSmallComet { get; private set; } = 3;
    }
}