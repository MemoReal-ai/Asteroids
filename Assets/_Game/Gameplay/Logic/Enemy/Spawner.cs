using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using Cysharp.Threading.Tasks;
using Zenject;
using Random = UnityEngine.Random;

namespace _Game.Gameplay.Logic.Enemy
{
    public class Spawner : ITickable
    {
        private List<ObjectPool<EnemyAbstract>> _pools;
        private SpawnerPoints _spawnerPoints;
        private readonly ShipAbstract _ship;
        private readonly float _timerToSpawn = 1.5f;
        private bool _isSpawning = false;

        public Spawner(List<ObjectPool<EnemyAbstract>> poolEnemy,SpawnerPoints spawnerPoints, ShipAbstract ship)
        {
            _spawnerPoints = spawnerPoints;
            _ship = ship;
            _pools = poolEnemy;
        }
        
        public void Tick()
        {
            if (_isSpawning == false)
            {
                _ = Spawn();
            }
        }

        async UniTask Spawn()
        {
            _isSpawning = true;
            var randomEnemyIndex = Random.Range(0, _pools.Count);
            var randomSpawnPointIndex = Random.Range(0, _spawnerPoints.Points.Count);
            var enemy = _pools[randomEnemyIndex].GetObject();
            
            enemy.Spawn(_spawnerPoints.Points[randomSpawnPointIndex].position, _ship);

            await UniTask.Delay(TimeSpan.FromSeconds(_timerToSpawn));

            _isSpawning = false;
        }
    }
}