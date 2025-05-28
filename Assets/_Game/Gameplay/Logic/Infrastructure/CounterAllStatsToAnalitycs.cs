using System;
using System.Collections.Generic;
using _Game.Firebase;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure
{
    public class CounterAllStatsToAnalitycs : IInitializable, IDisposable
    {
        private readonly DataStatsSDK _dataStatsSDK = new();
        private readonly Shoot _shoot;
        private readonly List<ObjectPool<EnemyAbstract>> _pools;
        private readonly IServiceAnalytics _serviceAnalytics;

        private string _dataJson;

        public CounterAllStatsToAnalitycs(Shoot shoot, IServiceAnalytics serviceAnalytics, List<ObjectPool<EnemyAbstract>> pools)
        {
            _serviceAnalytics = serviceAnalytics;
            _shoot = shoot;
            _pools = pools;
        }

        public void Initialize()
        {
            _shoot.OnShoot += _dataStatsSDK.AddCounterShoot;
            _shoot.OnLaserShoot += _dataStatsSDK.AddShootLaserCount;
            _shoot.OnLaserShoot += _serviceAnalytics.InvokeLaserShoot;
            foreach (var pool in _pools)
            {
                foreach (var enemy in pool.Objects)
                {
                    enemy.OnDeath += CounterEnemy;
                }
            }
        }

        public void Dispose()
        {
            _shoot.OnShoot -= _dataStatsSDK.AddCounterShoot;
            _shoot.OnLaserShoot -= _dataStatsSDK.AddShootLaserCount;
            _shoot.OnLaserShoot -= _serviceAnalytics.InvokeLaserShoot;

            foreach (var pool in _pools)
            {
                foreach (var enemy in pool.Objects)
                {
                    enemy.OnDeath -= CounterEnemy;
                }
            }


            _dataJson = JsonUtility.ToJson(_dataStatsSDK);
            _serviceAnalytics.InvokeStats(_dataJson);
        }

        private void CounterEnemy(EnemyAbstract enemy)
        {
            if (enemy is Comet)
            {
                _dataStatsSDK.AddDefeatComet();
            }
            else
            {
                _dataStatsSDK.AddDefeatUfo();
            }
        }
    }
}