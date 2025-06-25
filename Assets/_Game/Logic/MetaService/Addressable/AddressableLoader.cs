using System;
using System.Threading;
using _Game.Logic.UI.MainMenu.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

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

        public async UniTask<T> LoadPrefab<T>(FactoryUI factoryUI)
        {
            try
            {
                AsyncOperationHandle<Object> prefabTask = Addressables.LoadAssetAsync<Object>(typeof(T).Name);

                await prefabTask;

                if (prefabTask.Status == AsyncOperationStatus.Failed)
                {
                    throw new Exception("Failed to load prefab");
                }

                return await CreateOnSceneAddressablePrefab<T>(factoryUI, prefabTask);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        private async UniTask<T> CreateOnSceneAddressablePrefab<T>(FactoryUI factoryUI, AsyncOperationHandle<Object> prefabTask)
        {
            try
            {
                Object prefab = await prefabTask;
                var window = factoryUI.Create<T>(prefab);
                UnloadPrefab(prefab);
                return window;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void UnloadPrefab(Object prefab)
        {
            if (prefab)
            {
                Addressables.Release(prefab);
            }
        }
    }
}