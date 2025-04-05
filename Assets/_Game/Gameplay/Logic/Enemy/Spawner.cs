using System;
using _Game.Gameplay.Logic.Service.ObjectPool;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Gameplay.Logic.Enemy
{
    public class Spawner : ITickable
    {
        private ObjectPool<UFO> _enemieUFO;
        private ObjectPool<Comet> _enemieComet;
        private SpawnerPoints _spawnerPoints;
        private readonly float _timerToSpawn=1.5f;
        private bool _isSpawning;

        public Spawner(ObjectPool<UFO> enemiesUFO,ObjectPool<Comet> enemiesComet, SpawnerPoints spawnerPoints)
        {
            _enemieUFO = enemiesUFO;
            _enemieComet = enemiesComet;
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