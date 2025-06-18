using System;
using System.Threading;
using _Game.MainMenu.Logic.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Game.Logic.MetaService.Addressable
{
    public class AddressableLoader : IAddressableService, IDisposable
    {
        private CancellationTokenSource _tokenSource = new();

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = null;
        }

        public async UniTask<GameObject> LoadPrefab<T>(FactoryUI factoryUI)
        {
            try
            {
                AsyncOperationHandle<GameObject> prefabTask = Addressables.LoadAssetAsync<GameObject>(typeof(T).Name);

                await prefabTask;

                if (prefabTask.Status == AsyncOperationStatus.Failed)
                {
                    throw new Exception("Failed to load prefab");
                }

                return await CreateAddressablePrefab<T>(factoryUI, prefabTask);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        private async UniTask<GameObject> CreateAddressablePrefab<T>(FactoryUI factoryUI,AsyncOperationHandle<GameObject> prefabTask)
        {
            try
            {
                var prefab = await prefabTask;
                var window = factoryUI.Create(prefab);
                UnloadPrefab(prefab);
                return window;
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
                Addressables.Release(prefab);
            }
        }
    }
}