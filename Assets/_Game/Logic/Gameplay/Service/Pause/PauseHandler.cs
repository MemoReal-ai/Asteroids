using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.Gameplay.Service.Input;
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
        private readonly IInput _input;
        private readonly List<EnemyAbstract> _enemies = new();

        public PauseHandler(GameTimeHandler gameTimeHandler, Spawner spawner, ShipAbstract ship, Shoot shoot,
            List<ObjectPool<EnemyAbstract>> enemyPools, IInput input)
        {
            _gameTimeHandler = gameTimeHandler;
            _spawner = spawner;
            _ship = ship;
            _shoot = shoot;
            _enemyEnemyPools = enemyPools;
            _input = input;
        }

        public void Initialize()
        {
            _gameTimeHandler.OnPaused += PauseGame;
            _gameTimeHandler.OnResume += ResumeGame;
            _gameTimeHandler.OnLoseGame += PauseGame;
            _gameTimeHandler.OnPauseToAds += PauseGame;
            CastPoolToEnemyAbstract();
        }

        public void Dispose()
        {
            _gameTimeHandler.OnPaused -= PauseGame;
            _gameTimeHandler.OnResume -= ResumeGame;
            _gameTimeHandler.OnLoseGame -= PauseGame;
            _gameTimeHandler.OnPauseToAds -= PauseGame;
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

            _input.StopInput();
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

            _input.ResumeInput();
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