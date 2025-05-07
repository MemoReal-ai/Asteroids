using System.Collections.Generic;
using _Game.Firebase;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure
{
    public class EntryPoint : IInitializable

    {
        private readonly ObjectPool<Bullet> _objectPoolBulletDefault;
        private readonly ObjectPool<Bullet> _objectPoolBulletLaser;
        private readonly List<IWeapon> _weapons = new();
        private readonly Shoot _shoot;
        private readonly ShipAbstract _ship;
        private readonly List<ObjectPool<EnemyAbstract>> _pools;
        private readonly List<IWarping> _warpingCreature = new();
        private readonly Warp _warp;
        private readonly Camera _camera;
        private readonly IServiceSDK _serviceSDK;

        public EntryPoint([Inject(Id = EnumBullet.Default)] ObjectPool<Bullet> objectPoolBulletsDefault,
            [Inject(Id = EnumBullet.Laser)]
            ObjectPool<Bullet> objectPoolBulletLaser,
            List<ObjectPool<EnemyAbstract>> pools,
            Shoot shoot,
            ShipAbstract ship,
            SignalBus signalBus, Camera camera,
            Warp warp
            ,IServiceSDK serviceSDK)
        {
            _pools = pools;
            _objectPoolBulletLaser = objectPoolBulletLaser;
            _warp = warp;
            _objectPoolBulletDefault = objectPoolBulletsDefault;
            _ship = ship;
            _shoot = shoot;
            _serviceSDK = serviceSDK;
        }



        public void Initialize()
        {
            CreateWeapon();
            CastAllEnemiesToIWarping();
            _warp.Init(_warpingCreature);
            _shoot.Init(_weapons, _ship);
        }

        private void CreateWeapon()
        {
            var laserWeapon = new LaserWeapon(_objectPoolBulletLaser);
            var defaultWeapon = new WeaponDefault(_objectPoolBulletDefault);
            _weapons.Add(defaultWeapon);
            _weapons.Add(laserWeapon);
        }

        private void CastAllEnemiesToIWarping()
        {
            foreach (var enemyVariable in _pools)
            {
                foreach (var enemy in enemyVariable.Objects)
                {
                    _warpingCreature.Add(enemy);
                }
            }

            _warpingCreature.Add(_ship);
        }
    }
}