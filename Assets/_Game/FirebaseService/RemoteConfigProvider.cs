using System;
using System.Threading.Tasks;
using _Game.Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;
using Zenject;

namespace _Game.FirebaseService
{
    public class RemoteConfigProvider : IInitializable, IRemoteConfigProvider
    {
        public void Initialize()
        {
            FetchDataAsync();
        }

        public ScriptableObject GetRemoteConfig<T>(KeyToRemoteConfig key)
        {
            var jsonConfig=FirebaseRemoteConfig.DefaultInstance.GetValue(key.ToString()).StringValue;
            ScriptableObject newConfig = ScriptableObject.CreateInstance(typeof(T));
            JsonUtility.FromJsonOverwrite(jsonConfig, newConfig);
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