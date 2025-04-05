using UnityEngine;

namespace _Game.Gameplay.Logic.Ship
{
    public class StartSpawnPointShip : MonoBehaviour
    {
        [field: SerializeField]
        public Transform SpawnPoint { get; private set; }
    }
}