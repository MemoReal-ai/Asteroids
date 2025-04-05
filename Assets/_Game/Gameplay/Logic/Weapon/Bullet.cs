using System;
using _Game.Gameplay.Logic.Enemy;
using _Game.Gameplay.Logic.Service.ObjectPool;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace _Game.Gameplay.Logic.Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Bullet : MonoBehaviour, IPoolCreature
    {
        [SerializeField] private float _distanceToFade = 15f;
        [SerializeField] private float _speed = 3f;
        //Вынести в конфиг
        private Rigidbody2D _rigidbody;
        private Vector3 _startPosition;

        protected void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0;
        }

        protected void FixedUpdate()
        {
            Move();
            if (CheckDistance())
            {
                Fade();
            }
        }

        private void Move()
        {
            _rigidbody.AddForce(transform.right * _speed, ForceMode2D.Impulse);
        }

        private void OnDisable()
        {
            gameObject.transform.position = Vector2.zero;
            gameObject.transform.rotation = Quaternion.identity;
        }

        public virtual void Fade()
        {
            gameObject.SetActive(false);
        }

        protected bool CheckDistance()
        {
            var distance = (transform.position - _startPosition).magnitude;
            if (distance < _distanceToFade)
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