using System.Collections.Generic;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure
{
    public class EntryPoint : IInitializable, ITickable

    {
        private readonly ObjectPoolConfig<Bullet> _objectPoolBulletPrefab;
        private readonly ObjectPoolConfig<Bullet> _objectPoolLaserPrefab;
        private readonly List<IWeapon> _weapons = new();
        private readonly ObjectPoolConfigEnemy _objectPoolConfigUFO;
        private readonly ObjectPoolConfigEnemy _objectPoolConfigComet;
        private readonly Shoot _shoot;
        private readonly ShipAbstract _shootPoint;


        private Spawner _spawner;
        private List<ObjectPool<EnemyAbstract>> _pools = new();
        private ObjectPool<EnemyAbstract> _cometPool;
        private ObjectPool<EnemyAbstract> _ufoPool;
        private ObjectPool<Bullet> _bulletsDefault;
        private ObjectPool<Bullet> _bulletsLaser;
        private readonly SpawnerPoints _spawnerPoints;
        private readonly Containers _containers;

        public EntryPoint([Inject(Id = "Default")] ObjectPoolConfig<Bullet> objectPoolBulletPrefab, Shoot shoot,
            ShipAbstract shootPoint,
            [Inject(Id = "Laser")]
            ObjectPoolConfig<Bullet> objectPoolLaserPrefab,
            [Inject(Id = "Comet")]
            ObjectPoolConfigEnemy objectPoolConfigComet, [Inject(Id = "UFO")] ObjectPoolConfigEnemy objectPoolConfigUFO,
            SpawnerPoints spawnerPoints, Containers containers)
        {
            _objectPoolBulletPrefab = objectPoolBulletPrefab;
            _objectPoolLaserPrefab = objectPoolLaserPrefab;
            _shootPoint = shootPoint;
            _shoot = shoot;
            _objectPoolConfigUFO = objectPoolConfigUFO;
            _objectPoolConfigComet = objectPoolConfigComet;
            _spawnerPoints = spawnerPoints;
            _containers = containers;
        }

        private void CreateBullets()
        {
            _bulletsDefault = new ObjectPool<Bullet>(_objectPoolBulletPrefab.Object,
                _objectPoolBulletPrefab.ObjectSize,
                _containers.Bullet,
                _objectPoolBulletPrefab.AutoExpand);
            _bulletsLaser = new ObjectPool<Bullet>(_objectPoolLaserPrefab.Object,
                _objectPoolLaserPrefab.ObjectSize,
                _containers.Laser,
                _objectPoolLaserPrefab.AutoExpand);
        }

        private void CreateEnemies()
        {
            _ufoPool = new ObjectPool<EnemyAbstract>(_objectPoolConfigUFO.Object,
                _objectPoolConfigUFO.ObjectSize,
                _containers.UFO,
                _objectPoolConfigUFO.AutoExpand);
            _pools.Add(_ufoPool);
            _cometPool = new ObjectPool<EnemyAbstract>(_objectPoolConfigComet.Object,
                _objectPoolConfigComet.ObjectSize,
                _containers.Comet,
                _objectPoolConfigComet.AutoExpand);
            _pools.Add(_cometPool);
        }

        public void Initialize()
        {
            CreateBullets();
            CreateWeapon();
            CreateEnemies();
            _shoot.Init(_weapons, _shootPoint);
            _spawner = new Spawner(_pools, _spawnerPoints, _shootPoint);
        }

        private void CreateWeapon()
        {
            var laserWeapon = new LaserWeapon(_bulletsLaser);
            var defaultWeapon = new WeaponDefault(_bulletsDefault);
            _weapons.Add(defaultWeapon);
            _weapons.Add(laserWeapon);
        }


        public void Tick()
        {
            _spawner.Tick();
        }
    }
}