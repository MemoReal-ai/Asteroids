using System;
using _Game.Firebase;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;
using Zenject;

namespace _Game.Logic.MetaService.FirebaseService
{
    public class InitServiceAnalytics : IInitializable, IServiceAnalytics
    {
        private const string STARTGAME = "StartGame";
        private const string LASERSHOOT = "LaserShoot";
        private const string STATS = "Stats";
        private const string DATASTATSSDK = "DataStatsSDK";

        public void Initialize()
        {
            InitializeService();    
        }

        private async void InitializeService()
        {
            try
            {
                var status = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();

                if (status != DependencyStatus.Available)
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
            FirebaseAnalytics.LogEvent(STARTGAME, new Parameter("StartGame", "StartGame"));
        }

        public void InvokeStats(string dataStatsSDK)
        {
            FirebaseAnalytics.LogEvent(STATS, new Parameter(DATASTATSSDK, dataStatsSDK));
        }

        public void InvokeLaserShoot()
        {
            FirebaseAnalytics.LogEvent(LASERSHOOT, new Parameter("Shoot", "1"));
        }
    }
}