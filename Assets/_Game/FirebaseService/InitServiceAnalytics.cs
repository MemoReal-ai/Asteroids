using System;
using System.Threading.Tasks;
using Zenject;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace _Game.Firebase
{
    public class InitServiceAnalytics : IInitializable, IServiceAnalytics
    {
        private const string STARTGAME = "StartGame";
        private const string LASERSHOOT = "LaserShoot";
        private const string STATS = "Stats";
        private const string DATASTATSSDK = "DataStatsSDK";

        public void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(InitializeService);
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
                    throw new Exception("Failed to Available Firebase");
                }

            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        public void InvokeStartGame()
        {
            FirebaseAnalytics.LogEvent(STARTGAME,new Parameter("StartGame","StartGame"));
            Debug.Log("StartGame");
        }

        public void InvokeStats(string dataStatsSDK)
        {
            FirebaseAnalytics.LogEvent(STATS, new Parameter(DATASTATSSDK, dataStatsSDK));
            Debug.Log("Stats");
        }

        public void InvokeLaserShoot()
        {
            FirebaseAnalytics.LogEvent(LASERSHOOT, new Parameter("Shoot","1"));
            Debug.Log("Laser Shoot");
        }
    }
}