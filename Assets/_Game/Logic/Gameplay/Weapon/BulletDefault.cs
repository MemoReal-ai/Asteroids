using _Game.Gameplay.Logic.Enemy;
using _Game.Logic.Gameplay.Enemy;
using _Game.Logic.Gameplay.Weapon;
using UnityEngine;

namespace _Game.Gameplay.Logic.Weapon
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BulletDefault : Bullet

    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyAbstract enemy))
            {
                Fade();
            }
        }
    }
}