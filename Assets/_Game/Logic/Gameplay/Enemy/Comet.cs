using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.Gameplay.Weapon;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Game.Logic.Gameplay.Enemy
{
    public class Comet : EnemyAbstract
    {
        private CometConfig _cometConfig;
        private Vector3 _startPosition;
        private Vector3 _direction;
        private bool _initialized = false;
        private float _currentSpeed;
        private ObjectPool<SmallComet> _smallCometPool;

        [Inject]
        public void Construct(ObjectPool<SmallComet> cometPool)
        {
            _smallCometPool = cometPool;
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
            base.Initialize();
            _cometConfig = Provider.GetRemoteConfig<CometConfig>();
            _currentSpeed = Random.Range(_cometConfig.MinSpeed, _cometConfig.MaxSpeed);
            _initialized = true;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BulletDefault bullet))
            {
                Explode();
                CastAllDiedEffects();
            }

            if (other.TryGetComponent(out LaserBullet laserBullet))
            {
                CastAllDiedEffects();
            }
        }

        private void Explode()
        {
            for (int i = 0; i < _cometConfig.CountSmallComet; i++)
            {
                var angle = i * (360 / Random.value);
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
                var smallComet = _smallCometPool.GetObject();
                smallComet.Setup(direction, transform.position);
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