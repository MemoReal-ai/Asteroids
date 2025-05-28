using _Game.Gameplay.Logic.Weapon;
using UnityEngine;

namespace _Game
{
    public class JsonSerialize : MonoBehaviour
    {
        [SerializeField] private BulletStatsConfig _config;
        private void Start()
        {
            var jsonString = JsonUtility.ToJson(_config);
            Debug.Log(jsonString);
        }
    }
}
