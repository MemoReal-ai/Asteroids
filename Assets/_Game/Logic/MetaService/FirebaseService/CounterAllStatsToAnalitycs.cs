using System;
using System.Collections.Generic;
using _Game.Firebase;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Infrastructure;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.MetaService.JsonConvertHandler;
using Zenject;

namespace _Game.Logic.MetaService.FirebaseService
{
    public class CounterAllStatsToAnalitycs : IInitializable, IDisposable
    {
        private readonly DataStatsForAnalitycs _dataStatsForAnalitycs = new();
        private readonly Shoot _shoot;
        private readonly List<ObjectPool<EnemyAbstract>> _pools;
        private readonly IServiceAnalytics _serviceAnalytics;
        private readonly IJsonConverter _jsonConverter;

        private string _dataJson;

        public CounterAllStatsToAnalitycs(Shoot shoot, IServiceAnalytics serviceAnalytics,
            List<ObjectPool<EnemyAbstract>> pools, IJsonConverter jsonConverter)
        {
            _serviceAnalytics = serviceAnalytics;
            _shoot = shoot;
            _pools = pools;
            _jsonConverter = jsonConverter;
        }

        public void Initialize()
        {
            _shoot.OnShoot += _dataStatsForAnalitycs.AddCounterShoot;
            _shoot.OnLaserShoot += _dataStatsForAnalitycs.AddShootLaserCount;
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
            _shoot.OnShoot -= _dataStatsForAnalitycs.AddCounterShoot;
            _shoot.OnLaserShoot -= _dataStatsForAnalitycs.AddShootLaserCount;
            _shoot.OnLaserShoot -= _serviceAnalytics.InvokeLaserShoot;

            foreach (var pool in _pools)
            {
                foreach (var enemy in pool.Objects)
                {
                    enemy.OnDeath -= CounterEnemy;
                }
            }


            _dataJson = _jsonConverter.Serialize(_dataStatsForAnalitycs);
            _serviceAnalytics.InvokeStats(_dataJson);
        }

        private void CounterEnemy(EnemyAbstract enemy)
        {
            if (enemy is Comet)
            {
                _dataStatsForAnalitycs.AddDefeatComet();
            }
            else
            {
                _dataStatsForAnalitycs.AddDefeatUfo();
            }
        }
    }
}