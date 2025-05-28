using System;
using _Game.Firebase;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Game.Gameplay.Logic.Enemy
{
    public class Comet : EnemyAbstract
    {
        private CometConfig _cometConfig;
        private Vector3 _startPosition;
        private Vector3 _direction;
        private bool _initialized = false;
        private float _currentSpeed;
        private SmallComet _smallCometPrefab;

        [Inject]
        public void Construct(SmallComet comet)
        {
            _smallCometPrefab = comet;
        }
        private void Awake()
        {
            _cometConfig = Provider.GetRemoteConfig<CometConfig>(KeyToRemoteConfig.CometConfig);
        }

        private void OnEnable()
        {
            _currentSpeed = Random.Range(_cometConfig.MinSpeed, _cometConfig.MaxSpeed);
            _initialized = true;
        }

        protected override void Move()
        {
            if (_initialized)
            {
                _startPosition = transform.position;
                _direction = (TargetShip.transform.position - _startPosition).normalized;
                _initialized = false;
            }

            Rigidbody.AddForce(_direction * (_currentSpeed * Time.fixedDeltaTime), ForceMode2D.Force);
            Fade();
        }

        public override void Spawn(Vector3 position, ShipAbstract targetShip, SignalBus signalBus)
        {
            TargetShip = targetShip;
            transform.position = position;
            SignalBus = signalBus;
            gameObject.SetActive(true);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BulletDefault bullet))
            {
                Explode();
                InvokeOnDied();
                gameObject.SetActive(false);
            }

            if (other.TryGetComponent(out LaserBullet laserBullet))
            {
                InvokeOnDied();
                gameObject.SetActive(false);
            }
        }

        private void Explode()
        {
            for (int i = 0; i < _cometConfig.CountSmallComet; i++)
            {
                var angle = i * (360 / _cometConfig.CountSmallComet);
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
                var smallComet = Instantiator.InstantiatePrefabForComponent<SmallComet>(_smallCometPrefab,
                    transform.position,
                    Quaternion.identity,
                    null);
                smallComet.Setup(direction, SignalBus);
            }
        }

        private void Fade()
        {
            var magnitudeDistance = (transform.position - _startPosition).magnitude;

            if (magnitudeDistance > _cometConfig.DistanceToFade)
            {
                gameObject.SetActive(false);
            }
        }
    }
}