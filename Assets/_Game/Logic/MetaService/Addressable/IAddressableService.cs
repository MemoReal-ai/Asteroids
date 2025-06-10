using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Logic.MetaService.Addressable
{
    public interface IAddressableService
    {
        UniTask<GameObject> LoadPrefab<T>();
        void UnloadPrefab(GameObject prefab);
    }
}