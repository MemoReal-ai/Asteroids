using _Game.Logic.Gameplay.Service.ObjectPool;
using UnityEngine;

namespace _Game.Gameplay.Logic.Service.ObjectPool
{
    public abstract class ObjectPoolConfig<T> : ScriptableObject where T : IPoolCreature
    {
        [field: SerializeField]
        public T Object { get; private set; }
        [field: SerializeField]
        public int ObjectSize { get; private set; }
        [field: SerializeField]
        public bool AutoExpand { get; private set; }
    }
}