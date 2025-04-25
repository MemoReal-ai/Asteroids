using UnityEngine;

namespace _Game.Gameplay.Logic.Enemy
{
    [CreateAssetMenu(fileName = "SmallCometConfig",menuName = "Config/Enemy/SmallComet/Create", order = 0)]
    public class SmallCometConfig :ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float DistanceToDestroy { get; private set; }
        [field: SerializeField] public int Reward { get; private set; }
   
    }
}
