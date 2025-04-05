using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class UFO : MonoBehaviour, IPoolCreature, IEnemy
    {
        [SerializeField] private float _speed;
        //хз мб нужно вынести в конфиг
        private Rigidbody2D _rigidbody;
        private ShipAbstract _targetShip;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                gameObject.SetActive(false);
            }
        }

        [Inject]
        public void Construct(ShipAbstract targetShip)
        {
            _targetShip = targetShip;
        }

        public void Move()
        {
            Vector2 direction = (_targetShip.transform.position - transform.position).normalized;
            _rigidbody.MovePosition(_rigidbody.position + direction * (_speed * Time.deltaTime));
            Rotate(direction);
        }

        private void Rotate(Vector2 direction)
        {
            transform.right = direction;
        }
    }
}