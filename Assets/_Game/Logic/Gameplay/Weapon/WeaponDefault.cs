using _Game.Gameplay.Logic.Service.ObjectPool;
using Zenject;

namespace _Game.Logic.Gameplay.Weapon
{
    public class WeaponDefault : IWeapon
    {
        private readonly ObjectPool<Bullet> _bullets;

        public WeaponDefault([Inject(Id = EnumBullet.Default)] ObjectPool<Bullet> bullets)
        {
            _bullets = bullets;
        }

        public Bullet GetBullets()
        {
            return _bullets.GetObject();
        }
    }
}