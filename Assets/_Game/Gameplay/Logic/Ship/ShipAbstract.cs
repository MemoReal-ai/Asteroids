using _Game.Gameplay.Logic.Ship.Effects;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ShipAbstract : MonoBehaviour
    {
        private ShipConfig _shipConfig;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction;
        private float _currentRotationAngle;
        private EffectsMove _effectsMove;

        [field: SerializeField]
        public Transform ShipShootPoint { get; private set; }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0;
            _currentRotationAngle = transform.eulerAngles.z;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.AddForce(_direction * (_shipConfig.Speed * Time.fixedDeltaTime), ForceMode2D.Impulse);
            transform.rotation = Quaternion.Euler(0, 0, _currentRotationAngle);
        }

        [Inject]
        public void Construct(ShipConfig shipConfig)
        {
            _shipConfig = shipConfig;
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;

            if (direction == Vector3.zero)
            {
                return;
            }


            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _currentRotationAngle = Mathf.MoveTowardsAngle(_currentRotationAngle,
                angle,
                _shipConfig.RotationSpeed * Time.fixedDeltaTime);
        }
    }
}