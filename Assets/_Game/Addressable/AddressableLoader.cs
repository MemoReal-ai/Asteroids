using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Game.Addressable
{
    public class AddressableLoader : IAddressableService, IDisposable
    {
        private CancellationTokenSource _tokenSource;

        public async UniTask<GameObject> LoadPrefab(AssetReference prefab)
        {
            try
            {
                AsyncOperationHandle<GameObject> prefabTask = Addressables.LoadAssetAsync<GameObject>(prefab);

                await prefabTask;

                if (prefabTask.Status == AsyncOperationStatus.Failed)
                {
                    throw new Exception("Failed to load prefab");
                }

                return prefabTask.Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UnloadPrefab(GameObject prefab)
        {
            if (prefab)
            {
                Addressables.ReleaseInstance(prefab);
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = null;
        }
    }
}