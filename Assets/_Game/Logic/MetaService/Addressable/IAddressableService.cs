using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Addressable
{
    public interface IAddressableService
    {
        UniTask<GameObject> LoadPrefab<T>();
        void UnloadPrefab(GameObject prefab);
    }
}