using System;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using Zenject;

namespace _Game.Gameplay.Logic.UI
{
    public class PresenterViewUserUI : ITickable, IInitializable
    {
        private readonly UserView _userView;
        private readonly ShipAbstract _ship;
        private readonly Shoot _shoot;

        private bool _isSubscribe = false;

        public PresenterViewUserUI(UserView userView, ShipAbstract ship, Shoot shoot)
        {
            _userView = userView;
            _ship = ship;
            _shoot = shoot;
        }

        public void Tick()
        {
            _userView.SetCoordinate(_ship.transform.position.x, _ship.transform.position.y);
            _userView.SetVelocity(_ship.Rigidbody2D.linearVelocity.magnitude);
            _userView.SetAngleRotation(_ship.transform.eulerAngles.z);
            BulletCounter();
        }

        public void Initialize()
        {
            BulletCounter();
        }

        private void BulletCounter()
        {
            foreach (var weapon in _shoot.Weapons)
            {
                if (weapon is LaserWeapon laserWeapon)
                {
                    var counter = 0;
                    foreach (var bullet in laserWeapon.Bullets)
                    {
                        if (_isSubscribe != true)
                        {
                            if (bullet is LaserBullet laserBullet)
                            {
                                laserBullet.OnLaserReload += _userView.SetTimeReloadLaser;
                            }
                        }

                        if (bullet.IsAvailable)
                        {
                            counter++;
                        }
                    }

                    _userView.SetCountLaser(counter);
                }
            }

            _isSubscribe = false;
        }
    }
}