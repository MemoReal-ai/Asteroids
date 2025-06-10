using System;
using _Game.FirebaseService;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Infrastructure;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.Gameplay.Features;
using _Game.Logic.Gameplay.Service.ObjectPool;
using UnityEngine;
using Zenject;

namespace _Game.Logic.Gameplay.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SmallComet : MonoBehaviour, IEnemy, IPoolCreature
    {
        private SmallCometConfig _smallCometConfig;
        private Vector2 _direction;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _startPosition;
        private IRemoteConfigProvider _provider;
        private IScoreCounter _scoreCounter;

        [Inject]
        public void Construct(IRemoteConfigProvider provider, IScoreCounter scoreCounter)
        {
            _provider = provider;
            _scoreCounter = scoreCounter;
        }

        private void Start()
        {
            _smallCometConfig = _provider.GetRemoteConfig<SmallCometConfig>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0f;
        }

        private void FixedUpdate()
        {
            Move();
            if (TryDestroy())
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet _) || other.TryGetComponent(out EnemyAbstract _))
            {
                _scoreCounter.IncreaseScore(_smallCometConfig.Reward);
                gameObject.SetActive(false);
            }
        }

        private void Move()
        {
            _rigidbody2D.AddForce(_direction * _smallCometConfig.Speed, ForceMode2D.Impulse);
        }

        public void Setup(Vector2 direction, Transform spawnPoint)
        {
            _direction = direction;
            transform.position = spawnPoint.position;
            _startPosition = transform.position;
        }

        private bool TryDestroy()
        {
            var distance = (transform.position - _startPosition).magnitude;
            if (distance > _smallCometConfig.DistanceToDestroy)
            {
                return true;
            }

            return false;
        }
    }
}