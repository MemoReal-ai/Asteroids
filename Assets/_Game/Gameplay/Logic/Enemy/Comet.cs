using System;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace _Game.Gameplay.Logic.Enemy
{
    public class Comet : EnemyAbstract
    {
        [SerializeField] private float _distanceToFade;
        [SerializeField] private SmallComet _smallComet;
        [SerializeField] private float _minSpeed;
        [SerializeField] private int _countSmallComet = 3;
        // мейби вынести в конфиг
        private Vector3 _startPosition;
        private Vector3 _direction;
        private bool _initialized = false;

        private void OnValidate()
        {
            if (_minSpeed > _maxSpeed)
            {
                _maxSpeed = _minSpeed + 1;
            }
        }

        private void OnEnable()
        {
            _maxSpeed = Random.Range(_minSpeed, _maxSpeed);
        }

        protected override void Move()
        {
            if (_initialized == false)
            {
                _startPosition = transform.position;
                _direction = (TargetShip.transform.position - _startPosition).normalized;
                _initialized = true;
            } // Этот участок кода смущает :)

            Rigidbody.AddForce(_direction * (_maxSpeed * Time.fixedDeltaTime), ForceMode2D.Force);
            Fade();
        }

        public override void Spawn(Vector3 position, ShipAbstract targetShip)
        {
            TargetShip = targetShip;
            transform.position = position;
            gameObject.SetActive(true);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BulletDefault bullet))
            {
                Explode();
                gameObject.SetActive(false);
            }

            if (other.TryGetComponent(out LaserBullet laserBullet))
            {
                gameObject.SetActive(false);
            }
        }

        private void Explode()
        {
            for (int i = 0; i < _countSmallComet; i++)
            {
                var angle = i * (360 / _countSmallComet);
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
                var smallComet = Instantiate(_smallComet, transform.position, Quaternion.identity);
                smallComet.SetDirection(direction);
            }
        }

        private void Fade()
        {
            var magnitudeDistance = (transform.position - _startPosition).magnitude;

            if (magnitudeDistance > _distanceToFade)
            {
                gameObject.SetActive(false);
            }
        }
    }
}