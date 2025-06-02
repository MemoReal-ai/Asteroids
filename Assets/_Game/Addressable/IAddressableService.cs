using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Addressable
{
    public interface IAddressableService
    {
        UniTask<GameObject> LoadPrefab(NameAddressablePrefab prefabName);
        void UnloadPrefab(GameObject prefab);
    }
}