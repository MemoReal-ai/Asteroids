using _Game.Gameplay.Logic.Enemy;
using UnityEngine;

namespace _Game.Gameplay.Logic.Weapon
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BulletDefault : Bullet

    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemy enemy))
            {
                Fade();
            }
        }
    }
}