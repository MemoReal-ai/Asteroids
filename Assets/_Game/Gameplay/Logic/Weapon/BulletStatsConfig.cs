using UnityEngine;

namespace _Game.Gameplay.Logic.Weapon
{
    [CreateAssetMenu(fileName = "BulletStatsConfig", menuName = "Config/Bullet/Create")]
    public class BulletStatsConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float DistanceToFade { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }
 
    }
}
