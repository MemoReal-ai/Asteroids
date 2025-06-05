using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.ObjectPool;

namespace _Game.Gameplay.Logic.Weapon
{
    public class LaserWeapon : IWeapon
    {
        private readonly ObjectPool<Bullet> _bullets;

        public readonly IEnumerable<Bullet> Bullets;

        public LaserWeapon(ObjectPool<Bullet> bullets)
        {
            _bullets = bullets;
            Bullets = _bullets.Objects;
        }

        public Bullet GetBullets()
        {
            foreach (var bullet in _bullets.Objects)
            {
                if (bullet.IsAvailable == true && bullet.gameObject.activeInHierarchy == false)
                {
                    bullet.gameObject.SetActive(true);
                    return bullet;
                }
            }

            return null;
        }
    }
}