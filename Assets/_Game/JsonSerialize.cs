using _Game.Gameplay.Logic.Enemy;
using UnityEngine;

namespace _Game
{
    public class JsonSerialize : MonoBehaviour
    {
        [SerializeField] private SmallCometConfig _config;
        private void Start()
        {
            var jsonString = JsonUtility.ToJson(_config);
            Debug.Log(jsonString);
        }
    }
}
