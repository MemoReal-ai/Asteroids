
using UnityEngine;

namespace _Game.Gameplay.Logic.Weapon
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LaserBullet : Bullet
    {
        public override void Fade()
        {
            if (CheckDistance())
            {
                gameObject.SetActive(false);
            }
        }
    }
}