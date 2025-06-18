using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Infrastructure;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.Gameplay.Enemy;
using _Game.Logic.MetaService.JsonConvertHandler;
using Zenject;

namespace _Game.Logic.MetaService.FirebaseService
{
    public class CounterAllStatsToAnalitycs : IInitializable, IDisposable
    {
        private readonly DataStatsForAnalitycs _dataStatsForAnalitycs = new();
        private readonly Shoot _shoot;
        private readonly List<ObjectPool<EnemyAbstract>> _pools;
        private readonly IFirebaseServiceAnalytics _firebaseServiceAnalytics;
        private readonly IJsonConverter _jsonConverter;

        private string _dataJson;

        public CounterAllStatsToAnalitycs(Shoot shoot, IFirebaseServiceAnalytics firebaseServiceAnalytics,
            List<ObjectPool<EnemyAbstract>> pools, IJsonConverter jsonConverter)
        {
            _firebaseServiceAnalytics = firebaseServiceAnalytics;
            _shoot = shoot;
            _pools = pools;
            _jsonConverter = jsonConverter;
        }

        public void Initialize()
        {
            _shoot.OnShoot += _dataStatsForAnalitycs.AddCounterShoot;
            _shoot.OnLaserShoot += _dataStatsForAnalitycs.AddShootLaserCount;
            _shoot.OnLaserShoot += _firebaseServiceAnalytics.TrackLaserShoot;
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
            _shoot.OnLaserShoot -= _firebaseServiceAnalytics.TrackLaserShoot;

            foreach (var pool in _pools)
            {
                foreach (var enemy in pool.Objects)
                {
                    enemy.OnDeath -= CounterEnemy;
                }
            }


            _dataJson = _jsonConverter.Serialize(_dataStatsForAnalitycs);
            _firebaseServiceAnalytics.TrackStatsAfterLose(_dataJson);
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