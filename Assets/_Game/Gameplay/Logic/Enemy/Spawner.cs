using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Enemy
{
    public class Spawner : ITickable
    {
        private List<IEnemy> _enemies;
        private SpawnerPoints _spawnerPoints;
        private readonly float _timerToSpawn=1.5f;
        private bool _isSpawning;

        public Spawner(List<IEnemy> enemies, SpawnerPoints spawnerPoints)
        {
            _enemies = enemies;
            _spawnerPoints = spawnerPoints;
        }

        public void Tick()
        {
            if (_isSpawning == false)
            {
                _=Spawn();
            }
        }

        async UniTask Spawn()
        {
            _isSpawning = true;

            await UniTask.Delay(TimeSpan.FromSeconds(_timerToSpawn));

            _isSpawning = false;
        }
    }
}