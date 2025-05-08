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
    public class CounterAllStatsToSDK : IInitializable, IDisposable
    {
        private readonly DataStatsSDK _dataStatsSDK = new();
        private readonly Shoot _shoot;
        private readonly List<ObjectPool<EnemyAbstract>> _pools;
        private readonly IServiceSDK _serviceSDK;

        private string _dataJson;

        public CounterAllStatsToSDK(Shoot shoot, IServiceSDK serviceSDK, List<ObjectPool<EnemyAbstract>> pools)
        {
            _serviceSDK = serviceSDK;
            _shoot = shoot;
            _pools = pools;
        }

        public void Initialize()
        {
            _shoot.OnShoot += _dataStatsSDK.AddCounterShoot;
            _shoot.OnLaserShoot += _dataStatsSDK.AddShootLaserCount;
            _shoot.OnLaserShoot += _serviceSDK.InvokeLaserShoot;
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
            _shoot.OnLaserShoot -= _serviceSDK.InvokeLaserShoot;

            foreach (var pool in _pools)
            {
                foreach (var enemy in pool.Objects)
                {
                    enemy.OnDeath -= CounterEnemy;
                }
            }


            _dataJson = JsonUtility.ToJson(_dataStatsSDK);
            _serviceSDK.InvokeStats(_dataJson);
            Debug.Log(_dataJson);
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