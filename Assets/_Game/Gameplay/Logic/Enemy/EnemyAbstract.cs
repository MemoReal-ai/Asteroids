using System;
using _Game.Firebase;
using _Game.FirebaseService;
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

        protected DefaultEnemyConfig Config;
        protected Rigidbody2D Rigidbody;
        protected ShipAbstract TargetShip;
        protected SignalBus SignalBus;
        protected IInstantiator Instantiator;
        protected IRemoteConfigProvider Provider;


        private bool _isPaused;

        [Inject]
        public void Construct(IRemoteConfigProvider provider,IInstantiator instantiator)
        {
            Provider = provider;
            Instantiator = instantiator;
        }

        protected virtual void Start()
        {
            Config = Provider.GetRemoteConfig<DefaultEnemyConfig>(KeyToRemoteConfig.DefaultEnemyConfig);
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.gravityScale = 0;
        }

        private void FixedUpdate()
        {
            if (_isPaused == true)
            {
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
            SignalBus.Fire(new EnemyDiedSignal(Config.Reward));
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