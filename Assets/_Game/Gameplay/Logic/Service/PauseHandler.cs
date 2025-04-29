using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class PauseHandler : IInitializable, IDisposable
    {
        private readonly GameTimeHandler _gameTimeHandler;
        private readonly Spawner _spawner;
        private readonly ShipAbstract _ship;
        private readonly Shoot _shoot;
        private readonly List<ObjectPool<EnemyAbstract>> _enemyEnemyPools;

        private readonly List<EnemyAbstract> _enemies = new();

        public PauseHandler(GameTimeHandler gameTimeHandler, Spawner spawner, ShipAbstract ship, Shoot shoot,
            List<ObjectPool<EnemyAbstract>> enemyPools)
        {
            _gameTimeHandler = gameTimeHandler;
            _spawner = spawner;
            _ship = ship;
            _shoot = shoot;
            _enemyEnemyPools = enemyPools;
        }

        public void Initialize()
        {
            _gameTimeHandler.OnPaused += PauseGame;
            _gameTimeHandler.OnResume += ResumeGame;
            _gameTimeHandler.OnLoseGame += PauseGame;
            CastPoolToEnemyAbstract();
        }

        public void Dispose()
        {
            _gameTimeHandler.OnPaused -= PauseGame;
            _gameTimeHandler.OnResume -= ResumeGame;
            _gameTimeHandler.OnLoseGame -= PauseGame;
        }

        private void PauseGame()
        {
            _spawner.StopSpawning();
            _ship.PauseObject();
            _shoot.PauseObject();
            
            foreach (var enemy in _enemies)
            {
                enemy.PauseObject();
            }
        }

        private void ResumeGame()
        {
            _spawner.ResumeSpawning();
            _ship.ResumeObject();
            _shoot.ResumeObject();
            
            foreach (var enemy in _enemies)
            {
                enemy.ResumeObject();
            }
        }

        private void CastPoolToEnemyAbstract()
        {
            foreach (var pool in _enemyEnemyPools)
            {
                foreach (var enemy in pool.Objects)
                {
                    _enemies.Add(enemy);
                }
            }
        }
    }
}