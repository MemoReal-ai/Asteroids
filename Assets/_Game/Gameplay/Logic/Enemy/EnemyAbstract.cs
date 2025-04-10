using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;

namespace _Game.Gameplay.Logic.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class EnemyAbstract : MonoBehaviour, IPoolCreature,IEnemy
    {
        [SerializeField] protected float _maxSpeed;
        //хз мб нужно вынести в конфиг
        protected Rigidbody2D Rigidbody;
        protected ShipAbstract TargetShip;

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
                gameObject.SetActive(false);
            }
        }

        public virtual void Spawn(Vector3 position, ShipAbstract targetShip)
        {
        }


        protected abstract void Move();
    }
}