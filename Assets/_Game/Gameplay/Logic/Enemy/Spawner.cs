using System;
using System.Collections.Generic;
using System.Threading;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Game.Gameplay.Logic.Enemy
{
    public class Spawner : ITickable, IInitializable, IDisposable
    {
        private const int CountSide = 4;
        private const float SpawnCameraOffset = 5f;

        private bool _isSpawning = false;
        private float _cameraBoundsTop;
        private float _cameraBoundsBottom;
        private float _cameraBoundsLeft;
        private float _cameraBoundsRight;
        private CancellationTokenSource _cancellationToken = new();
        
        private bool _stopSpawnOnPause = false;

        private readonly List<ObjectPool<EnemyAbstract>> _pools;
        private readonly ShipAbstract _ship;
        private readonly float _timerToSpawn = 1.5f;
        private readonly SignalBus _signalBus;
        private readonly Camera _camera;




        public Spawner(List<ObjectPool<EnemyAbstract>> poolEnemy, ShipAbstract ship,
            SignalBus signalBus, Camera camera)
        {
            _ship = ship;
            _pools = poolEnemy;
            _signalBus = signalBus;
            _camera = camera;
        }

        public void Initialize()
        {
            CalculateCameraBounds();
        }


        public void Tick()
        {
            if (_stopSpawnOnPause)
            {
                return;
            }

            if (_isSpawning == false)
            {
                _ = Spawn();
            }
        }

        public void Dispose()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
            _cancellationToken = null;
        }

        public void StopSpawning()
        {
            _stopSpawnOnPause = true;
        }

        public void ResumeSpawning()
        {
            _stopSpawnOnPause = false; 
        }

        private async UniTask Spawn()
        {
            _isSpawning = true;
            try
            {
                var randomEnemyIndex = Random.Range(0, _pools.Count);
                var randomSideIndex = Random.Range(0, CountSide);

                var enemy = _pools[randomEnemyIndex].GetObject();

                enemy.Spawn(GetPositionToSpawn(randomSideIndex), _ship, _signalBus);

                await UniTask.Delay(TimeSpan.FromSeconds(_timerToSpawn), cancellationToken: _cancellationToken.Token);
            }
            catch (InvalidOperationException)
            {
            }

            _isSpawning = false;
        }

        private void CalculateCameraBounds()
        {
            var cameraHeight = 2 * _camera.orthographicSize;
            var cameraWidth = cameraHeight * _camera.aspect;

            _cameraBoundsLeft = _camera.transform.position.x - cameraHeight / 2;
            _cameraBoundsRight = _camera.transform.position.x + cameraWidth / 2;
            _cameraBoundsTop = _camera.transform.position.y + cameraHeight / 2;
            _cameraBoundsBottom = _camera.transform.position.y - cameraHeight / 2;
        }

        private Vector3 GetPositionToSpawn(int numberToChoiceSide)
        {
            float x;
            float y;
            switch (numberToChoiceSide)
            {
                case 0: //left
                    x = _cameraBoundsLeft - SpawnCameraOffset;
                    y = Random.Range(_cameraBoundsBottom, _cameraBoundsTop);
                    return new Vector3(x, y, 0);
                case 1: //right
                    x = _cameraBoundsRight + SpawnCameraOffset;
                    y = Random.Range(_cameraBoundsBottom, _cameraBoundsTop);
                    return new Vector3(x, y, 0);
                case 2: //up
                    x = Random.Range(_cameraBoundsLeft, _cameraBoundsRight);
                    y = _cameraBoundsTop + SpawnCameraOffset;
                    return new Vector3(x, y, 0);
                case 3: //bottom
                    x = Random.Range(_cameraBoundsLeft, _cameraBoundsRight);
                    y = _cameraBoundsBottom - SpawnCameraOffset;
                    return new Vector3(x, y, 0);
                default:
                    return Vector3.zero;
            }
        }
    }
}