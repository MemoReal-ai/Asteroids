using UnityEngine;

namespace _Game.Gameplay.Logic.Ship
{
    [CreateAssetMenu(fileName = "ShipConfig", menuName = "Config/Ship Config/Create")]
    public class ShipConfig : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField, Min(1)]
        public float Speed { get; private set; }
        [field: SerializeField]
        public float RotationSpeed { get; private set; }
        [field: SerializeField]
        public float StopAcceleration { get; private set; }
    }
}