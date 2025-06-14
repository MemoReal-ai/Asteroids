using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.UI;
using _Game.Gameplay.Logic.UI.AdsView;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.Gameplay.Features;
using _Game.Logic.Gameplay.Service.Sound;
using _Game.Logic.MetaService.Addressable;
using _Game.MainMenu.Logic.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure
{
    public class EntryPointGamePlayScene : IInitializable, IDisposable

    {
        private readonly ObjectPool<Bullet> _objectPoolBulletDefault;
        private readonly ObjectPool<Bullet> _objectPoolBulletLaser;
        private readonly IAddressableService _addressableService;
        private readonly FactoryUI _factoryUI;
        private readonly List<IWeapon> _weapons = new();
        private readonly Shoot _shoot;
        private readonly ShipAbstract _ship;
        private readonly List<ObjectPool<EnemyAbstract>> _pools;
        private readonly List<IWarping> _warpingCreature = new();
        private readonly Warp _warp;
        private readonly Camera _camera;
        private readonly List<GameObject> _addressableResources = new();
        private readonly SoundHandler _soundHandler;

        public EntryPointGamePlayScene([Inject(Id = EnumBullet.Default)] ObjectPool<Bullet> objectPoolBulletsDefault,
            [Inject(Id = EnumBullet.Laser)]
            ObjectPool<Bullet> objectPoolBulletLaser,
            List<ObjectPool<EnemyAbstract>> pools,
            Shoot shoot,
            ShipAbstract ship, Camera camera,
            Warp warp, IAddressableService addressableService, FactoryUI factoryUI, SoundHandler soundHandler)
        {
            _factoryUI = factoryUI;
            _addressableService = addressableService;
            _pools = pools;
            _objectPoolBulletLaser = objectPoolBulletLaser;
            _warp = warp;
            _objectPoolBulletDefault = objectPoolBulletsDefault;
            _ship = ship;
            _shoot = shoot;
            _soundHandler = soundHandler;
        }


        public void Initialize()
        {
            CreateWeapon();
            CastAllEnemiesToIWarping();
            _warp.Init(_warpingCreature);
            _shoot.Init(_weapons, _ship,_soundHandler);
            InitUI();
        }

        public void Dispose()
        {
            foreach (var gameObject in _addressableResources)
            {
                _addressableService.UnloadPrefab(gameObject);
            }
        }

        private async void InitUI()
        {
            try
            {
                await CreateAddressablePrefab<UserView>();
                await CreateAddressablePrefab<LoseView>();
                await CreateAddressablePrefab<AdsPopupView>();
                await CreateAddressablePrefab<PauseView>();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
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

        private async UniTask CreateAddressablePrefab<T>()
        {
            try
            {
                var prefab = await _addressableService.LoadPrefab<T>();
                _factoryUI.Create(prefab);
                _addressableResources.Add(prefab);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}