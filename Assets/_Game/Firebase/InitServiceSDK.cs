using System;
using System.Threading;
using System.Threading.Tasks;
using Zenject;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace _Game.Firebase
{
    public class InitServiceSDK : IInitializable, IServiceSDK, IDisposable
    {
        private const string STARTGAME = "StartGame";
        private const string STOPGAME = "StopGame";
        private const string STATS = "Stats";
        private const string DATASTATSSDK = "DataStatsSDK";

        private CancellationTokenSource _tokenSource = new();

        public void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(InitializeService);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = null;
        }

        private void InitializeService(Task<DependencyStatus> task)
        {
            try
            {
                if (!task.IsCompletedSuccessfully)
                {
                    throw new Exception("Failed to initialize Firebase");
                }

                if (task.Result != DependencyStatus.Available)
                {
                    throw new Exception("Failed to initialize Firebase");
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        public void InvokeStartGame()
        {
            FirebaseAnalytics.LogEvent(STARTGAME);
            Debug.Log("StartGame");
        }

        public void InvokeStats(string dataStatsSDK)
        {
            FirebaseAnalytics.LogEvent(STATS, new Parameter(DATASTATSSDK, dataStatsSDK));
            Debug.Log("Stats");
        }

        public void InvokeEndGame()
        {
            FirebaseAnalytics.LogEvent(STOPGAME);
            Debug.Log("EndGame");
        }
    }
}