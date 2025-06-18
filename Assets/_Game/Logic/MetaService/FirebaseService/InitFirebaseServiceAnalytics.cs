using System;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;
using Zenject;

namespace _Game.Logic.MetaService.FirebaseService
{
    public class InitFirebaseServiceAnalytics : IInitializable, IFirebaseServiceAnalytics
    {
        private const string START_GAME = "StartGame";
        private const string LASER_SHOOT = "LaserShoot";
        private const string STATS = "Stats";
        private const string DATA_STATS_SDK = "DataStatsSDK";

        public void Initialize()
        {
            _ = InitializeService();
        }

        private async UniTask InitializeService()
        {
            try
            {
                var status = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();

                if (status != DependencyStatus.Available)
                {
                    throw new Exception("Failed to Available Firebase");
                }

                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        public void TrackStartGame()
        {
            FirebaseAnalytics.LogEvent(START_GAME, new Parameter("StartGame", "StartGame"));
        }

        public void TrackStatsAfterLose(string dataStatsSDK)
        {
            FirebaseAnalytics.LogEvent(STATS, new Parameter(DATA_STATS_SDK, dataStatsSDK));
        }

        public void TrackLaserShoot()
        {
            FirebaseAnalytics.LogEvent(LASER_SHOOT, new Parameter("Shoot", "1"));
        }
    }
}