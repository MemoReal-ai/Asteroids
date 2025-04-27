using System.Collections.Generic;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.UI;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Infrastructure
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private ShipDefault _shipDefault;
        [SerializeField] private StartSpawnPointShip _startSpawnPointShip;
        [SerializeField] private ShipConfig _shipConfig;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private ObjectPoolConfigBullet _objectPoolConfigBullet;
        [SerializeField] private ObjectPoolConfigBullet _objectPoolConfigBulletLaser;
        [SerializeField] private ObjectPoolConfigEnemy _objectPoolConfigUFO;
        [SerializeField] private ObjectPoolConfigEnemy _objectPoolConfigComet;
        [SerializeField] private LoseView _loseView;
        [SerializeField] private UserView _userView;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private Containers _containers;

        private IInstantiator _instantiator;

        private ObjectPool<Bullet> _bulletPoolDefault;
        private ObjectPool<Bullet> _bulletPoolLaser;
        private ObjectPool<EnemyAbstract> _objectPoolUFO;
        private ObjectPool<EnemyAbstract> _objectPoolComet;
        private List<ObjectPool<EnemyAbstract>> _poolsEnemies = new();

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            InstallStartGame();
            InstallShip();
            CreateAndBindObjectPools();
            InstallSpawn();
            InstallUI();
        }

        private void InstallStartGame()
        {
            Container.Bind<SceneHandler>().AsCached();
            Container.BindInterfacesTo<EntryPoint>().AsCached();
            Container.BindInterfacesTo<PauseHandler>().AsCached();
            Container.BindInterfacesAndSelfTo<GameTimeHandler>().AsSingle().NonLazy();
            Container.Bind<Shoot>().AsCached();
            Container.BindInterfacesAndSelfTo<KeyboardInput>().AsCached();
            Container.BindInterfacesTo<InputPlayer>().AsCached();
            Container.BindInterfacesAndSelfTo<Warp>().AsCached();
            Container.Bind<Camera>().FromInstance(_mainCamera).AsCached();
            Container.DeclareSignal<EnemyDiedSignal>();
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsCached();
        }

        private void InstallShip()
        {
            Container.BindInterfacesAndSelfTo<ShipAbstract>().FromComponentInNewPrefab(_shipDefault)
                .UnderTransform(_startSpawnPointShip.SpawnPoint.transform).AsCached().NonLazy();
            Container.Bind<ShipConfig>().FromInstance(_shipConfig).AsSingle();
        }

        private void InstallSpawn()
        {
            Container.BindInterfacesAndSelfTo<Spawner>().AsCached();
        }

        private void CreateAndBindObjectPools()
        {
            _instantiator = Container.Resolve<IInstantiator>();

            _bulletPoolDefault = new ObjectPool<Bullet>(_objectPoolConfigBullet.Object,
                _objectPoolConfigBullet.ObjectSize,
                _containers.Bullet,
                _objectPoolConfigBullet.AutoExpand,
                _instantiator);
            Container.Bind<ObjectPool<Bullet>>().WithId(EnumBullet.Default).FromInstance(_bulletPoolDefault).AsCached();

            _bulletPoolLaser = new ObjectPool<Bullet>(_objectPoolConfigBulletLaser.Object,
                _objectPoolConfigBulletLaser.ObjectSize,
                _containers.Laser,
                _objectPoolConfigBulletLaser.AutoExpand,
                _instantiator);
            Container.Bind<ObjectPool<Bullet>>().WithId(EnumBullet.Laser).FromInstance(_bulletPoolLaser).AsCached();

            _objectPoolComet = new ObjectPool<EnemyAbstract>(_objectPoolConfigComet.Object,
                _objectPoolConfigComet.ObjectSize,
                _containers.Comet,
                _objectPoolConfigComet.AutoExpand,
                _instantiator);

            _poolsEnemies.Add(_objectPoolComet);

            _objectPoolUFO = new ObjectPool<EnemyAbstract>(_objectPoolConfigUFO.Object,
                _objectPoolConfigUFO.ObjectSize,
                _containers.UFO,
                _objectPoolConfigUFO.AutoExpand,
                _instantiator);
            
            _poolsEnemies.Add(_objectPoolUFO);
            //Жестко нарушение DRY но увы хз как сделать лучше;
            Container.Bind<List<ObjectPool<EnemyAbstract>>>().FromInstance(_poolsEnemies).AsCached();
        }

        private void InstallUI()
        {
            Container.BindInterfacesAndSelfTo<ViewModelLose>().AsCached();
            Container.BindInterfacesAndSelfTo<ViewModelUserStats>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseViewModel>().AsCached();
            Container.BindInterfacesAndSelfTo<PresenterReloadView>().AsCached();

            Container.Bind<LoseView>().FromComponentInNewPrefab(_loseView).AsCached();
            Container.Bind<PauseView>().FromComponentInNewPrefab(_pauseView).AsCached();
            Container.Bind<UserView>().FromComponentsInNewPrefab(_userView).AsCached();
            Container.Bind<ReloadView>().FromComponentInNewPrefab(_userView).AsCached();
        }
    }
}