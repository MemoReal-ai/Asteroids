using _Game.FirebaseService;
using _Game.Gameplay.Logic.Service.ObjectPool;
using UnityEngine;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace _Game.Gameplay.Logic.Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Bullet : MonoBehaviour, IPoolCreature
    {
        protected BulletStatsConfig BulletStatsConfig;

        private Rigidbody2D _rigidbody;
        private Vector3 _startPosition;
        private IRemoteConfigProvider _configProvider;

        public bool IsAvailable { get; protected set; } = true;

        [Inject]
        public void Construct(IRemoteConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        protected void Start()
        {
            Initialize();
        }

        protected void FixedUpdate()
        {
            Move();
            if (CheckDistance())
            {
                Fade();
            }
        }

        private void Initialize()
        {
            BulletStatsConfig = _configProvider.GetRemoteConfig<BulletStatsConfig>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0;
        }

        private void Move()
        {
            _rigidbody.AddForce(transform.right * BulletStatsConfig.Speed, ForceMode2D.Impulse);
        }

        protected virtual void OnDisable()
        {
            gameObject.transform.position = Vector2.zero;
            gameObject.transform.rotation = Quaternion.identity;
        }

        protected virtual void Fade()
        {
            gameObject.SetActive(false);
        }

        protected bool CheckDistance()
        {
            var distance = (transform.position - _startPosition).magnitude;
            if (distance < BulletStatsConfig.DistanceToFade)
            {
                return false;
            }

            return true;
        }

        public void SetTransform(Transform targetTransform)
        {
            _startPosition = transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
        }
    }
}