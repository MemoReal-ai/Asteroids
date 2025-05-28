using System;
using _Game.Firebase;
using _Game.FirebaseService;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Features;
using _Game.Gameplay.Logic.Ship.Effects;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ShipAbstract : MonoBehaviour, IWarping
    {
        public event Action OnLoseLastLife;
        public event Action OnShipDestroyedToRewardAds;
        
        private ShipConfig _shipConfig;
        private Vector2 _direction;
        private float _inputDirection;
        private float _currentRotationAngle;
        private EffectsMove _effectsMove;
        private float _targetAngle;
        private bool _isPaused = false;
        private Vector2 _linearVelocityBeforePause;
        private int _counterDeath = 0;
        private IRemoteConfigProvider _provider;

        public Rigidbody2D Rigidbody2D { get; private set; }

        [field: SerializeField]
        public Transform ShipShootPoint { get; private set; }

        [Inject]
        public void Construct(IRemoteConfigProvider provider)
        {
            _provider = provider;
        }

        protected virtual void Start()
        {
            _shipConfig = _provider.GetRemoteConfig<ShipConfig>(KeyToRemoteConfig.ShipConfig);
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Rigidbody2D.gravityScale = 0;
            _currentRotationAngle = transform.eulerAngles.z;
        }

        private void FixedUpdate()
        {
            if (_isPaused == true)
            {
                return;
            }

            HandleMovement();
            RotateShip();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemy _))
            {
                if (_counterDeath == 0)
                {
                    OnShipDestroyedToRewardAds?.Invoke();
                    _counterDeath++;
                }
                else
                {
                    OnLoseLastLife?.Invoke();
                    _counterDeath=0;
                }
            }
        }

        private void HandleMovement()
        {
            if (_inputDirection < 0)
            {
                Rigidbody2D.linearVelocity = Vector2.Lerp(Rigidbody2D.linearVelocity,
                    Vector2.zero,
                    _shipConfig.StopAcceleration * Time.fixedDeltaTime);
                return;
            }

            _direction = transform.right * (_inputDirection * (_shipConfig.Speed * Time.fixedDeltaTime));
            Rigidbody2D.AddForce(_direction, ForceMode2D.Impulse);
        }

        public void SetDirection(float direction)
        {
            _inputDirection = direction;
        }

        public void SetRotationAngle(float directionRotation)
        {
            _targetAngle = directionRotation;
        }

        private void RotateShip()
        {
            _currentRotationAngle -= _targetAngle * _shipConfig.RotationSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0, 0, _currentRotationAngle);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void SetPosition(Vector3 warpingPosition)
        {
            transform.position = warpingPosition;
        }

        public void InvokeLoseLastLife()
        {
            OnLoseLastLife?.Invoke();
        }

        public void PauseObject()
        {
            _isPaused = true;
            _linearVelocityBeforePause = Rigidbody2D.linearVelocity;
            Rigidbody2D.linearVelocity = Vector2.zero;
        }

        public void ResumeObject()
        {
            _isPaused = false;
            Rigidbody2D.linearVelocity = _linearVelocityBeforePause;
        }
    }
}