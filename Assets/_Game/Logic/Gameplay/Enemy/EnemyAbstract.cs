using System;
using _Game.FirebaseService;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.Gameplay.Enemy;
using _Game.Logic.Gameplay.Features;
using _Game.Logic.Gameplay.Service.ObjectPool;
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
        protected IRemoteConfigProvider Provider;
        protected IScoreCounter ScoreCounter;

        private bool _isPaused;

        [Inject]
        public void Construct(IRemoteConfigProvider provider, IScoreCounter scoreCounter)
        {
            Provider = provider;
            ScoreCounter = scoreCounter;
        }

        protected virtual void OnEnable()
        {
            Initialize();
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
            if (other.TryGetComponent(out Bullet _) || other.TryGetComponent(out SmallComet _))
            {
                InvokeOnDied();
                gameObject.SetActive(false);
            }
        }

        public abstract void Spawn(Vector3 position, ShipAbstract targetShip);

        protected abstract void Move();

        protected virtual void Initialize()
        {
            Config = Provider.GetRemoteConfig<DefaultEnemyConfig>();
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.gravityScale = 0;
        }

        protected void InvokeOnDied()
        {
            ScoreCounter.IncreaseScore(Config.Reward);
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