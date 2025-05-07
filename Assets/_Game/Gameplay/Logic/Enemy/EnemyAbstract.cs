using System;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Infrastructure;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class EnemyAbstract : MonoBehaviour, IPoolCreature, IEnemy, IWarping
    {
        public event Action<EnemyAbstract> OnDeath;

        [SerializeField] protected float _maxSpeed;
        [SerializeField] protected int _reward;

        protected Rigidbody2D Rigidbody;
        protected ShipAbstract TargetShip;
        protected SignalBus SignalBus;

        private bool _isPaused;
        private Vector2 _linearVelocityBeforePause;

        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.gravityScale = 0;
        }

        private void FixedUpdate()
        {
            if (_isPaused == true)
            {
                _linearVelocityBeforePause = Rigidbody.linearVelocity;
                Rigidbody.linearVelocity = Vector2.zero;
                return;
            }

            Move();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet) || other.TryGetComponent(out SmallComet smallComet))
            {
                InvokeOnDied();
                gameObject.SetActive(false);
            }
        }


        public abstract void Spawn(Vector3 position, ShipAbstract targetShip, SignalBus signalBus);

        protected abstract void Move();

        protected void InvokeOnDied()
        {
            SignalBus.Fire(new EnemyDiedSignal(_reward));
            OnDeath?.Invoke(this);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void SetPosition(Vector3 warpingPosition)
        {
            transform.position = warpingPosition;
        }
        
        public void PauseObject()
        {
            _isPaused = true;
        }

        public void ResumeObject()
        {
            _isPaused = false;
        }
    }
}