using System;
using _Game.Firebase;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.Gameplay.Enemy;
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
        private ObjectPool<SmallComet> _cometPool;

        [Inject]
        public void Construct(ObjectPool<SmallComet> cometPool)
        {
            _cometPool = cometPool;
        }

        public override void Spawn(Vector3 position, ShipAbstract targetShip)
        {
            TargetShip = targetShip;
            transform.position = position;
            gameObject.SetActive(true);
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
            Rigidbody.linearVelocity = Vector3.ClampMagnitude(_direction, _currentSpeed);
            Fade();
        }


        protected override void Initialize()
        {
            _cometConfig = Provider.GetRemoteConfig<CometConfig>();
            _currentSpeed = Random.Range(_cometConfig.MinSpeed, _cometConfig.MaxSpeed);
            _initialized = true;
            base.Initialize();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BulletDefault bullet))
            {
                Explode();
                InvokeOnDied();
                SoundHandler.PlayAudioDead();
                gameObject.SetActive(false);
            }

            if (other.TryGetComponent(out LaserBullet laserBullet))
            {
                InvokeOnDied();
                SoundHandler.PlayAudioDead();
                gameObject.SetActive(false);
            }
        }

        private void Explode()
        {
            for (int i = 0; i < _cometConfig.CountSmallComet; i++)
            {
                var angle = i * (360 / Random.value);
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
                var smallComet = _cometPool.GetObject();
                smallComet.Setup(direction, transform);
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