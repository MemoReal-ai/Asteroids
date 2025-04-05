using System.Collections.Generic;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure
{
    public class EntryPoint : IInitializable

    {
        private readonly ObjectPoolConfig<Bullet> _objectPoolBulletPrefab;
        private readonly ObjectPoolConfig<Bullet> _objectPoolLaserPrefab;
        private readonly List<IWeapon> _weapons = new();
        
        
        private ObjectPool<Bullet> _bulletsDefault;
        private ObjectPool<Bullet> _bulletsLaser;
        private readonly Shoot _shoot;
        private readonly ShipAbstract _shootPoint;

        public EntryPoint([Inject(Id = "Default")] ObjectPoolConfig<Bullet> objectPoolBulletPrefab, Shoot shoot,
            ShipAbstract shootPoint,
            [Inject(Id = "Laser")]
            ObjectPoolConfig<Bullet> objectPoolLaserPrefab)
        {
            _objectPoolBulletPrefab = objectPoolBulletPrefab;
            _objectPoolLaserPrefab = objectPoolLaserPrefab;
            _shootPoint = shootPoint;
            _shoot = shoot;
        }

        private void CreateBullets()
        {
            _bulletsDefault = new ObjectPool<Bullet>(_objectPoolBulletPrefab.Object,
                _objectPoolBulletPrefab.ObjectSize,
                null,
                _objectPoolBulletPrefab.AutoExpand);
            _bulletsLaser = new ObjectPool<Bullet>(_objectPoolLaserPrefab.Object,
                _objectPoolLaserPrefab.ObjectSize,
                null,
                _objectPoolLaserPrefab.AutoExpand);
        }

        private void CreateEnemies()
        {
            
        }

        public void Initialize()
        {
            CreateBullets();
            CreateWeapon();
            _shoot.Init(_weapons, _shootPoint);
        }

        private void CreateWeapon()
        {
            var laserWeapon = new LaserWeapon(_bulletsLaser);
            var defaultWeapon = new WeaponDefault(_bulletsDefault);
            _weapons.Add(defaultWeapon);
            _weapons.Add(laserWeapon);
        }
    }
}