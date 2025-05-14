using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Game.Addressable
{
    public interface IAddressableService
    {
        UniTask<GameObject> LoadPrefab(AssetReference prefab);

        UniTask UnloadPrefab(GameObject prefab);
    }
}