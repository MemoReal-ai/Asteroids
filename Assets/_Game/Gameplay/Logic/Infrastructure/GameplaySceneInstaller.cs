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
        [SerializeField] private SpawnerPoints _spawnerPoints;
        [SerializeField] private ObjectPoolConfigEnemy _objectPoolConfigUFO;
        [SerializeField] private ObjectPoolConfigEnemy _objectPoolConfigComet;
        [SerializeField] private LoseView _loseView;
        [SerializeField] private UserView _userView;
        [SerializeField] private PauseView _pauseView;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            InstallStartGame();
            InstallSpawn();
            InstallShip();
            InstallUI();
        }

        private void InstallStartGame()
        {
            Container.Bind<ObjectPoolConfig<Bullet>>().WithId("Default").FromInstance(_objectPoolConfigBullet)
                .AsCached();
            Container.Bind<ObjectPoolConfig<Bullet>>().WithId("Laser").FromInstance(_objectPoolConfigBulletLaser)
                .AsCached();
            Container.Bind<Containers>().FromComponentInHierarchy().AsCached();
            Container.Bind<SceneHandler>().AsCached();
            Container.BindInterfacesTo<EntryPoint>().AsCached();
            Container.BindInterfacesTo<GameTimeHandler>().AsCached();
            Container.Bind<Shoot>().AsCached();
            Container.BindInterfacesAndSelfTo<InputPlayer>().AsCached();
            Container.BindInterfacesTo<Warp>().AsCached();
            Container.Bind<Camera>().FromInstance(_mainCamera).AsCached();
            Container.Bind<GameTimeHandler>().AsCached();
            Container.DeclareSignal<EnemyDiedSignal>();
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsCached();
        }

        private void InstallShip()
        {
            Container.Bind<ShipAbstract>().FromComponentInNewPrefab(_shipDefault)
                .UnderTransform(_startSpawnPointShip.SpawnPoint.transform).AsCached();
            Container.Bind<ShipConfig>().FromInstance(_shipConfig).AsCached();
        }

        private void InstallSpawn()
        {
            Container.Bind<SpawnerPoints>().FromInstance(_spawnerPoints).AsCached();
            Container.Bind<ObjectPoolConfigEnemy>().WithId("UFO").FromInstance(_objectPoolConfigUFO).AsCached();
            Container.Bind<ObjectPoolConfigEnemy>().WithId("Comet").FromInstance(_objectPoolConfigComet).AsCached();
        }

        private void InstallUI()
        {
            Container.BindInterfacesAndSelfTo<ViewModelLose>().AsCached();
            Container.BindInterfacesAndSelfTo<ViewModelUserStats>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseViewModel>().AsCached();
            Container.BindInterfacesAndSelfTo<PresenterReloadView>().AsCached();
           
            var loseView = Container.InstantiatePrefabForComponent<LoseView>(_loseView);
            Container.Bind<LoseView>().FromInstance(loseView).AsCached();
            var userView = Container.InstantiatePrefabForComponent<UserView>(_userView);
            Container.Bind<UserView>().FromInstance(userView).AsCached();
            var pauseView = Container.InstantiatePrefabForComponent<PauseView>(_pauseView);
            Container.Bind<PauseView>().FromInstance(pauseView).AsCached();
            var reloadView = userView.GetComponent<ReloadView>();
            Container.Bind<ReloadView>().FromInstance(reloadView).AsCached();
            Container.BindInterfacesAndSelfTo<HandlerPopup>().AsCached();
        }
    }
}