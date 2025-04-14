using System;
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
    public abstract class EnemyAbstract : MonoBehaviour, IPoolCreature, IEnemy
    {
        [SerializeField] protected float _maxSpeed;
        [SerializeField] protected int _reward;

        protected Rigidbody2D Rigidbody;
        protected ShipAbstract TargetShip;
        protected SignalBus SignalBus;

        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.gravityScale = 0;
        }

        private void FixedUpdate()
        {
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


        public virtual void Spawn(Vector3 position, ShipAbstract targetShip, SignalBus signalBus)
        {
        }


        protected abstract void Move();

        protected void InvokeOnDied()
        {
            SignalBus.Fire(new EnemyDiedSignal(_reward));
        }
    }
}