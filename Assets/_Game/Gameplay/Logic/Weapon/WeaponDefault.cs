using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Service.ObjectPool;

namespace _Game.Gameplay.Logic.Weapon
{
    public class WeaponDefault : IWeapon
    {
        private readonly ObjectPool<Bullet> _bullets;


        public WeaponDefault(ObjectPool<Bullet> bullets)
        {
            _bullets = bullets;
        }

        public Bullet GetBullets()
        {
         return _bullets.GetObject();
        }
    }
}