using System;
using System.Threading.Tasks;
using _Game.Firebase;
using _Game.Gameplay.Logic.Service;
using _Game.Logic.MetaService.JsonConvertHandler;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;
using Zenject;

namespace _Game.FirebaseService
{
    public class RemoteConfigProvider : IInitializable, IRemoteConfigProvider
    {
        private readonly IJsonConverter _jsonConverter;

        public RemoteConfigProvider(IJsonConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }

        public void Initialize()
        {
            FetchDataAsync();
        }

        public T GetRemoteConfig<T>()
        {
            var jsonConfig = FirebaseRemoteConfig.DefaultInstance.GetValue(typeof(T).Name).StringValue;
            T newConfig = _jsonConverter.Deserialize<T>(jsonConfig);
            return newConfig;
        }

        private Task FetchDataAsync()
        {
            var task = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
            return task.ContinueWithOnMainThread(FetchComplete);
        }

        private void FetchComplete(Task task)
        {
            try
            {
                if (!task.IsCompleted)
                {
                    throw new Exception("Fetch task is not completed");
                }

                var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
                var info = remoteConfig.Info;

                if (info.LastFetchStatus != LastFetchStatus.Success)
                {
                    throw new Exception("Remote config could not be fetched");
                }

                remoteConfig.ActivateAsync().ContinueWithOnMainThread(task =>
                {
                    Debug.Log("Remote config activated");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}