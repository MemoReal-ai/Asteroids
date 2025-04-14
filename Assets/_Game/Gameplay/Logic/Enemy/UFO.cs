using _Game.Gameplay.Logic.Service.ObjectPool;
using _Game.Gameplay.Logic.Ship;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Enemy
{
    public class UFO : EnemyAbstract, IPoolCreature
    {
        protected override void Move()
        {
            Vector2 direction = (TargetShip.transform.position - transform.position).normalized;
            Rigidbody.MovePosition(Rigidbody.position + direction * (_maxSpeed * Time.deltaTime));
            Rotate(direction);
        }

        private void Rotate(Vector2 direction)
        {
            transform.right = direction;
        }

        public override void Spawn(Vector3 position, ShipAbstract targetShip,SignalBus signalBus)
        {
            TargetShip = targetShip;
            transform.position = position;
            SignalBus=signalBus;
            gameObject.SetActive(true);
        }
    }
}