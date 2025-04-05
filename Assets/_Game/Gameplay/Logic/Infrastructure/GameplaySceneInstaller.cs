using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
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
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private BulletConfig _bulletConfigLaser;
        [SerializeField] private SpawnerPoints _spawnerPoints;

        public override void InstallBindings()
        {
            InstallStartGame();
            InstallShip();
        }

        private void InstallStartGame()
        {
            Container.Bind<ObjectPoolConfig<Bullet>>().WithId("Default").FromInstance(_bulletConfig).AsCached();
            Container.Bind<ObjectPoolConfig<Bullet>>().WithId("Laser").FromInstance(_bulletConfigLaser).AsCached();
            Container.BindInterfacesTo<EntryPoint>().AsCached();
            Container.Bind<Shoot>().AsCached();
            Container.BindInterfacesTo<InputPlayer>().AsCached();
            Container.BindInterfacesTo<Warp>().AsCached();
            Container.Bind<Camera>().FromInstance(_mainCamera).AsCached();
        }

        private void InstallShip()
        {
            Container.Bind<ShipAbstract>().FromComponentInNewPrefab(_shipDefault)
                .UnderTransform(_startSpawnPointShip.SpawnPoint.transform).AsCached();
            Container.Bind<ShipConfig>().FromInstance(_shipConfig).AsCached();
        }

        private void InstallSpawn()
        {
            Container.BindInterfacesTo<Spawner>().AsCached();
            Container.Bind<SpawnerPoints>().FromInstance(_spawnerPoints).AsCached();
        }
    }
}