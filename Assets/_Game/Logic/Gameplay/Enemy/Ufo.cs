using _Game.Gameplay.Logic.Ship;
using UnityEngine;

namespace _Game.Gameplay.Logic.Enemy
{
    public class Ufo : EnemyAbstract
    {
        protected override void Move()
        {
            Vector2 direction = (TargetShip.transform.position - transform.position).normalized;
            Rigidbody.MovePosition(Rigidbody.position + direction * (Config.Speed * Time.deltaTime));
            Rotate(direction);
        }

        private void Rotate(Vector2 direction)
        {
            transform.right = direction;
        }

        public override void Spawn(Vector3 position, ShipAbstract targetShip)
        {
            TargetShip = targetShip;
            transform.position = position;
            gameObject.SetActive(true);
        }
    }
}