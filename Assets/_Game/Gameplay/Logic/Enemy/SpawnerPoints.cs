using System.Collections.Generic;
using UnityEngine;

namespace _Game.Gameplay.Logic.Enemy
{
    public class SpawnerPoints : MonoBehaviour
    {
        [field:SerializeField] public List<Transform> Points{ get; private set; }
    }
}