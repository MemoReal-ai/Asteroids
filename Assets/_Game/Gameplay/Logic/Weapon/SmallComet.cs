using System;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SmallComet : MonoBehaviour, IEnemy
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _distanceToFade = 10f;
        [SerializeField] private int _reward=1;
        private Vector2 _direction;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _startPosition;
        private SignalBus _signalBus;
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0f;
            _startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            Move();
            if (TryFade())
            {
                _signalBus.Fire(new EnemyDiedSignal(_reward));
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet) || other.TryGetComponent(out EnemyAbstract enemy))
            {
                gameObject.SetActive(false);
            }
        }

        private void Move()
        {
            _rigidbody2D.AddForce(_direction * _speed);
        }

        public void Setup(Vector2 direction,SignalBus signalBus)
        {
            _direction = direction;
            _signalBus = signalBus;
        }

        private bool TryFade()
        {
            var distance = (transform.position - _startPosition).magnitude;
            if (distance > _distanceToFade)
            {
                return true;
            }

            return false;
        }
    }
}