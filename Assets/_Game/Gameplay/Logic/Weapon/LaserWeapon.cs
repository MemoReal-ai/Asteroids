using _Game.Gameplay.Logic.Service;

namespace _Game.Gameplay.Logic.Weapon
{
    public class LaserWeapon : IWeapon
    {
        private ObjectPool<Bullet> _bullets;

        public LaserWeapon(ObjectPool<Bullet> bullets)
        {
            _bullets = bullets;
        }

        public Bullet GetBullets()
        {
         return  _bullets.GetObject();
        }
    }
}